// <copyright file="ParseRequestParamsAttribute.cs" company="Lime-Tec AG"> 
// Copyright (C) Lime-Tec AG, Switzerland - All Rights Reserved. 
// Unauthorized copying of this file, via any medium is strictly prohibited. 
// Proprietary and confidential. 
// </copyright> 

using Infrastructure.Exceptions;
using Infrastructure.QueryParamsParser;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ResQue.Infrastructure
{
    /// <summary> 
    /// Parses Request URI parameters in order to get filtering, sorting, pagination settings. 
    /// </summary> 
    /// <seealso cref="ActionFilterAttribute" /> 
    public sealed class ParseRequestParamsAttribute : ActionFilterAttribute
    {
        private readonly JsonApiQueryParser _jsonApiQueryParser;

        public ParseRequestParamsAttribute(JsonApiQueryParser jsonApiQueryParser)
        {
            _jsonApiQueryParser = jsonApiQueryParser ??
                throw new ArgumentNullException(nameof(jsonApiQueryParser));
        }

        /// <summary> 
        /// This method is executed before Controller's Action. 
        /// </summary> 
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var (multipleFilteringOptions, sortingOptions, paginationOptions) =
                _jsonApiQueryParser.Parse();

            var filteringParam = context.ActionArguments.FirstOrDefault(pair => pair.Value is List<FilteringOptions>);

            if (filteringParam.Key != null)
            {
                context.ActionArguments[filteringParam.Key] = multipleFilteringOptions;
            }

            var sortingParam = context.ActionArguments.FirstOrDefault(pair => pair.Value is SortingOptions);
            if (sortingParam.Key != null)
            {
                context.ActionArguments[sortingParam.Key] = sortingOptions;
            }

            var paginationParam = context.ActionArguments.FirstOrDefault(pair => pair.Value is PaginationOptions);
            if (paginationParam.Key != null)
            {
                context.ActionArguments[paginationParam.Key] = paginationOptions;
            }
        }
    }
}
 
 
namespace ResQue.Infrastructure
{
    /// <summary> 
    /// Provides functionality for GET query parameters parsing and for generation of options for 
    /// data retrieving from ReadModel 
    /// </summary> 
    public sealed class JsonApiQueryParser
    {
        private const string SortingQuery = "sort";
        private const string FilteringQuery = "filter";
        private const string PaginationQuery = "page";
        private const string PageSizeQuery = "page[size]";
        private const string PageNumberQuery = "page[number]";
        private const string AttributeGroupName = "attribute";
        private const string ValueGroupName = "value";
        private const string ComparisonGroupName = "comparison";
        private const string FilteringAttributePattern = @"filter\[(?<attribute>(?:\w+-?\.?)+\w+)\]";
        private const string FilteringComparisonAndValuePattern = @"(?<comparison>eq:|ne:|le:|ge:|lt:|gt:|like:)?(?<value>(\w|-|.)+)";
        private const char DescendingIndicator = '-';
        private const byte PositionAfterDescendingIndicator = 1;

        //private readonly ComparisonTypeMapper _comparisonTypeMapper;
        private HttpContext _httpContext;

        private readonly List<FilteringOptions> _multipleFilteringOptions =
            new List<FilteringOptions>();

        private SortingOptions _sortingOptions;
        private PaginationOptions _paginationOptions;

        public JsonApiQueryParser(IHttpContextAccessor httpContextAccessor
            /*ComparisonTypeMapper comparisonTypeMapper*/)
        {
            if (httpContextAccessor is null)
            {
                throw new ArgumentNullException(nameof(httpContextAccessor));
            }

            _httpContext = httpContextAccessor.HttpContext;
            //_comparisonTypeMapper = comparisonTypeMapper ??
            //    throw new ArgumentNullException(nameof(comparisonTypeMapper));
        }

        public (IReadOnlyList<FilteringOptions>, SortingOptions, PaginationOptions) Parse()
        {
            IQueryCollection queryCollection = _httpContext.Request.Query;

            foreach (string queryKey in queryCollection.Keys)
            {
                switch (queryKey)
                {
                    case SortingQuery:
                        HandleSortingQuery(queryCollection[queryKey]);
                        break;

                    // Used .Contains() because query should be like "?filter[attribute]=eq:value" 
                    //case var key when key != null && key.Contains(FilteringQuery):
                    //    HandleFilteringQuery(queryKey, queryCollection[queryKey]);
                    //    break;

                    // Used .Contains() because query should be like "?page[size]=10&page[number]=2" 
                    case var key when key != null && key.Contains(PaginationQuery):
                        HandlePaginationQueryParam(queryKey, queryCollection[queryKey]);
                        break;
                }
            }

            return (_multipleFilteringOptions, _sortingOptions, _paginationOptions);
        }

        private void HandleSortingQuery(StringValues queryValues)
        {
            if (queryValues.Count == 0)
            {
                throw new BadRequestException($"{nameof(queryValues)} should not be empty");
            }

            string queryValue = queryValues.First();

            if (queryValue == string.Empty)
            {
                throw new BadRequestException("Sorting parameter should not be empty");
            }

            bool isDescending = queryValue[0] == DescendingIndicator;

            _sortingOptions = new SortingOptions
            {
                AttributeName = isDescending
                    ? queryValue.Substring(PositionAfterDescendingIndicator, queryValue.Length - PositionAfterDescendingIndicator)
                    : queryValue,

                IsDescending = isDescending
            };
        }

        //private void HandleFilteringQuery(string queryKeyWithAttribute, StringValues queryValues)
        //{
        //    GroupCollection attributeGroups =
        //        GetFilteringGroupCollection(queryKeyWithAttribute, FilteringAttributePattern);

        //    var filteringOptions = new FilteringOptions(attributeGroups[AttributeGroupName].Value);

        //    // Query for the same attribute with multiple parameters is in format "filter[number]=ge:1,le:5" 
        //    IEnumerable<string> queries = SplitFilters(queryValues).Distinct();

        //    foreach (string queryValue in queries)
        //    {
        //        GroupCollection comparisonAndValueGroups =
        //            GetFilteringGroupCollection(queryValue, FilteringComparisonAndValuePattern);

        //        string comparisonGroupValue = comparisonAndValueGroups[ComparisonGroupName].Value;

        //        ComparisonType comparisonType = comparisonGroupValue == string.Empty
        //            ? ComparisonType.Equal
        //            : _comparisonTypeMapper.Map(comparisonGroupValue.Remove(comparisonGroupValue.Length - 1)); // Remove ':', because it is like 'eq:' 

        //        // Applies "AND" logical operator for filtering. Query string should be in format 
        //        // "filter[number]=ge:1,le:5" Notice that for "AND" you should use different 
        //        // comparison types for the same property (in current example: ge, le). 
        //        if (!filteringOptions.Filters.Any() ||
        //            filteringOptions.Filters.Values.Any(ct => ct == comparisonType))
        //        {
        //            filteringOptions.AddFilter(comparisonAndValueGroups[ValueGroupName].Value,
        //                comparisonType);
        //        }
        //        // Applies "OR" logical operator for filtering. Query string should be in format 
        //        // "filter[name]=like:A,like:B" Notice that for "OR" you should use the same 
        //        // comparison types for the same property (in current example: like). 
        //        else
        //        {
        //            _multipleFilteringOptions.Add(new FilteringOptions(attributeGroups[AttributeGroupName].Value,
        //                comparisonAndValueGroups[ValueGroupName].Value,
        //                comparisonType));
        //        }
        //    }

        //    if (!filteringOptions.IsEmpty)
        //    {
        //        _multipleFilteringOptions.Add(filteringOptions);
        //    }

        //    IReadOnlyList<string> SplitFilters(StringValues queryValues)
        //    {
        //        string pattern = @",(eq:)|,(ne:)|,(le:)|,(ge:)|,(lt:)|,(gt:)|,(like:)";

        //        string[] substrings = Regex.Split(queryValues, pattern);

        //        List<string> queries = new List<string> { string.Empty };

        //        //Adding filtering value to comparison type. 
        //        for (int i = 0; i < substrings.Length; i++)
        //        {
        //            if (i % 2 != 0)
        //                queries.Add(substrings[i]);
        //            else
        //                queries[i - i / 2] += substrings[i];
        //        }

        //        return queries;
        //    }
        //}

        private void HandlePaginationQueryParam(string paramName, StringValues queryValues)
        {
            if (queryValues.Count == 0)
            {
                throw new BadRequestException($"{nameof(queryValues)} should not be empty");
            }

            string queryValue = queryValues.First();

            if (!int.TryParse(queryValue, out var convertedValue))
            {
                throw new BadRequestException($"{queryValue} is incorrect pagination parameter");
            }

            if (convertedValue < 0)
            {
                throw new BadRequestException($"{nameof(queryValue)} should be more or equal to zero");
            }

            if (_paginationOptions == null)
            {
                _paginationOptions = new PaginationOptions();
            }

            switch (paramName)
            {
                case PageSizeQuery:
                    _paginationOptions.PageSize = convertedValue;
                    break;

                case PageNumberQuery:
                    _paginationOptions.PageNumber = convertedValue;
                    break;

                default:
                    throw new BadRequestException($"Unsupported pagination parameter: {paramName}");
            }
        }

        private GroupCollection GetFilteringGroupCollection(string matchString, string pattern)
        {
            var regex = new Regex(pattern);

            if (!regex.IsMatch(matchString))
            {
                throw new BadRequestException($"{matchString} contains wrong filtering parameters");
            }

            return regex.Match(matchString).Groups;
        }
    }
}