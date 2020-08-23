using System;
using System.Collections.Generic;
using System.Text;

namespace CasaDoCupom.Domain.Interface.Model
{
    public interface IModel<Tkey>
    {
        Tkey Id { get; set; }
    }
}
