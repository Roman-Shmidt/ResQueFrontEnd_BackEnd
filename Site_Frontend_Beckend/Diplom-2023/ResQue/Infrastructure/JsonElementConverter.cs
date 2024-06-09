using Microsoft.AspNetCore.Mvc.Filters;
using ResQueBackEnd.Managers.MenuDomain;
using System.Reflection;
using System.Text.Json;

namespace ResQue.Infrastructure
{
    public static class JsonElementConverter<T> where T: class
    {
        public static IDictionary<string, object> ConvertUpdatedValues(IDictionary<string, object> updatedValuesUnconverted)
        {
            Dictionary<string, JsonElement> updatedValues = new();
            foreach (var item in updatedValuesUnconverted)
            {
                updatedValues.Add(item.Key, (JsonElement)item.Value);
            }

            var updatedValuesAsObjects = new Dictionary<string, object>();
            foreach (var kvp in updatedValues)
            {
                string fieldName = kvp.Key;
                JsonElement jsonValue = kvp.Value;

                // Використовуємо рефлексію для отримання поля за його назвою
                PropertyInfo propertyInfo = typeof(T).GetProperty(fieldName);
                if (propertyInfo != null)
                {
                    // Отримуємо тип поля
                    Type fieldType = propertyInfo.PropertyType;

                    if (fieldType == typeof(string) && jsonValue.ValueKind == JsonValueKind.String)
                    {
                        updatedValuesAsObjects[kvp.Key] = jsonValue.GetString();
                    }
                    else if (fieldType == typeof(TimeSpan) && jsonValue.ValueKind == JsonValueKind.String)
                    {
                        updatedValuesAsObjects[kvp.Key] = TimeSpan.Parse(jsonValue.ToString());
                    }
                    else if (fieldType == typeof(int) && jsonValue.ValueKind == JsonValueKind.Number)
                    {
                        updatedValuesAsObjects[kvp.Key] = jsonValue.GetInt32();
                    }
                    else if (fieldType == typeof(decimal) && jsonValue.ValueKind == JsonValueKind.Number)
                    {
                        updatedValuesAsObjects[kvp.Key] = jsonValue.GetDecimal();
                    }
                    else if (fieldType == typeof(bool))
                    {
                        updatedValuesAsObjects[kvp.Key] = jsonValue.GetBoolean();
                    }
                    else
                    {
                        throw new ArgumentException($"Unsupported JSON value kind: {kvp.Value.ValueKind}");
                    }

                }
            }
            return updatedValuesAsObjects;
        }
    }
}
