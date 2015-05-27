using FW.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.Data.Mapping
{
    public class BaseEntityTypeConfiguration<T> : EntityTypeConfiguration<T>
        where T : BaseEntity
    {
        public BaseEntityTypeConfiguration()
        {
            this.ToTable(typeof(T).Name);
            this.HasKey(x => x.Id);
        }
    }
}
