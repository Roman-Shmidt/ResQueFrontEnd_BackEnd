namespace ResQue.Infrastructure
{
    public class ReturnObject<T>
        where T : class
    {
        public ReturnObject(T? returnObject,
            string errorMessage,
            string errorKey) 
        { 
            Object = returnObject;
            ErrorMessage = errorMessage;
            ErrorKey = errorKey;
        }

        public T? Object { get; set; }

        public string ErrorMessage { get; set; }

        public string ErrorKey { get; set; }
    }
}
