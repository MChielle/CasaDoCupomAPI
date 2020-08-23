using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CasaDoCupom.Domain.Entities.CRUDBase
{
    public class EntityCRUD<Tkey> : Entity<Tkey>
    {
        public override Tkey Id { get => base.Id; set => base.Id = value; }

        private DateTime _dataUltimaAlteracao;

        [Column(TypeName = "DATETIME")]
        public DateTime DataUltimaAlteracao
        {
            get { return _dataUltimaAlteracao; }
            set { _dataUltimaAlteracao = DateTime.Now; }
        }

        private DateTime _dataCriacao;

        [Column(TypeName = "DATETIME")]
        public DateTime DataCriacao
        {
            get { return _dataCriacao; }
            set { _dataCriacao = (value == default ? DateTime.Now : value); }
        }
    }
}