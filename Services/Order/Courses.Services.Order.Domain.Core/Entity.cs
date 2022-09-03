namespace Courses.Services.Order.Domain.Core
{
    public abstract class Entity
    {
        private int? _requestedHashCode;
        private int _Id;

        public virtual int Id
        {
            get => _Id;
            set => _Id = value;
        }

        //burada default bir değer varsa
        //geçici mi olup olmadığına bakıyor
        public bool IsTransient()
        {
            return this.Id == default(Int32);
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = this.Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

                return _requestedHashCode.Value;
            }
            else
                return base.GetHashCode();
        }

        //iki object in birbirine eşit olup olmadığa bakar
        //tipinin referansın aynı zamanda id sinin
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity))
                return false;

            if (Object.ReferenceEquals(this, obj))
                return true;

            if (this.GetType() != obj.GetType())
                return false;

            Entity item = (Entity)obj;

            if (item.IsTransient() || this.IsTransient())
                return false;
            else
                return item.Id == this.Id;
        }

        //karşılaştırma yapıyor aslında soldaki ile sağdaki arasında
        public static bool operator ==(Entity left, Entity right)
        {
            if (Object.Equals(left, null))
                return (Object.Equals(right, null)) ? true : false;
            else
                return left.Equals(right);
        }

        //eşit değil mi diye karşılaştırıyor
        public static bool operator !=(Entity left, Entity right)
        {
            if (Object.Equals(left, null))
                return (Object.Equals(right, null)) ? true : false;
            else
                return left.Equals(right);
        }
    }
}