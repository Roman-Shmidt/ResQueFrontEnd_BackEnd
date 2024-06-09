using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueDal.Common.Repositories
{
    public static class RepositoryErrors
    {
        /// <summary> 
        /// The can not delete because entity is not found error message. 
        /// </summary> 
        public const string CanNotDeleteBecauseEntityIsNotFoundErrorMessage = "Can not delete because entity is not found";

        /// <summary> 
        /// The can not delete because entity is not found error key. 
        /// </summary> 
        public const string CanNotDeleteBecauseEntityIsNotFoundErrorKey = "CAN_NOT_DELETE_ENTITY_IS_NOT_FOUND";

        /// <summary> 
        /// The can not update because entity is not found error message. 
        /// </summary> 
        public const string CanNotUpdateBecauseEntityIsNotFoundErrorMessage = "Can not update because entity is not found";

        /// <summary> 
        /// The can not update because entity is not found error key. 
        /// </summary> 
        public const string CanNotUpdateBecauseEntityIsNotFoundErrorKey = "CAN_NOT_UPDATE_ENTITY_IS_NOT_FOUND";

        /// <summary> 
        /// The error during saving to database error key. 
        /// </summary> 
        public const string ErrorDuringSavingToDbErrorKey = "ERROR_DURING_SAVING_TO_DB";

        /// <summary> 
        /// The error during saving to database error message. 
        /// </summary> 
        public const string ErrorDuringSavingToDbErrorMessage = "Error during saving to DB";
    }
}
