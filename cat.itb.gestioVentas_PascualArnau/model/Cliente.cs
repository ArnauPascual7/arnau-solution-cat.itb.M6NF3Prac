namespace cat.itb.gestioVentas_PascualArnau.model
{
    public class Cliente
    {
        public virtual int _id { get; set; }
        public virtual string Nombre { get; set; }
        public virtual string Apellido1 { get; set; }
        public virtual string Apellido2 { get; set; }
        public virtual string Ciudad { get; set; }
        public virtual int Categoria { get; set; }
    }
}
