using System;

namespace Domain.Data
{
    public abstract partial class BaseEntity
    {
        protected BaseEntity()
        {
            _id = UniqueIdentifier.New;
        }

        public DateTime CreatedOnUtc { get; set; }
        //{
        //    get { return _createdOnUtc; }
        //    private set
        //    {
        //        _createdOnUtc = DateTime.UtcNow;
        //    }
        //}

        public string Id
        {
            get { return _id; }
            private set
            {
                if (string.IsNullOrEmpty(value))
                    _id = UniqueIdentifier.New;
                else
                    _id = value;
            }
        }

        private DateTime _createdOnUtc;
        private string _id;
    }
}
