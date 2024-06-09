using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ResQueBackEnd.Common.Dto
{
    /// <summary> 
    /// Contains base properties of DTO. 
    /// </summary> 
    [DataContract]
    public abstract class BaseDto : IDto,
        IResourceWithPossibleActions
    {
    
        //private Dictionary<string, string> _possibleActions;

        ///// <summary> 
        ///// The possible actions for current state of Resource. 
        ///// </summary> 
        //public IReadOnlyDictionary<string, string> PossibleActions
        //{
        //    get => _possibleActions;
        //    set => _possibleActions = value?.ToDictionary(pair => pair.Key, pair => pair.Value);
        //}

        ///// <summary> 
        ///// Adds the possible action. 
        ///// </summary> 
        ///// <param name="possibleActionName">Name of the possible action.</param> 
        ///// <param name="possibleActionLink">The possible action link.</param> 
        //public void AddPossibleAction(string possibleActionName,
        //    string possibleActionLink)
        //{
        //    if (_possibleActions == null)
        //    {
        //        _possibleActions = new Dictionary<string, string>();
        //    }

        //    _possibleActions.Add(possibleActionName, possibleActionLink.ToString());
        //}

    }
}
