//using System;

//namespace NogupeWeb.Entities
//{
//    public abstract class AuditableEntity<T> : Entity<T>, IAuditableEntity
//    {
//        public DateTime CreatedDate { get; set; } = DateTime.Now;

//        public int CreatedBy { get; set; }

//        public DateTime? UpdatedDate { get; set; }

//        public int? UpdatedBy { get; set; }

//        public bool IsDeleted { get; set; }
//    }
//}