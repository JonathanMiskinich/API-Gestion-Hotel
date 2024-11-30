namespace HotelManagement.Models
{
    public abstract class SoftDelete
    {
        protected bool _isDeleted = false;
        protected DateOnly FechaEliminacion { get; set; }

        public bool  isDeleted
        {
            get => this._isDeleted;
            set
            {
                if (this._isDeleted != value)
                    this._isDeleted = value;
            }
        }
    }
}