using ApiDotNet6.Domain.Validations;

namespace ApiDotNet6.Domain.Entities
{
    public sealed class Product
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string CodErp { get; private set; }
        public decimal Price { get; private set; }
        public ICollection<Purchase> Purchases { get; set; }

        public Product(string name, string codErp, decimal price)
        {
            Validation(name, codErp, price);
            Purchases = new List<Purchase>();
        }

        public Product(int id, string name, string codErp, decimal price)
        {
            DomainValidationException.When(id < 0, "Id deve ser informado!");
            Id = id;
            Validation(name, codErp, price);
            Purchases = new List<Purchase>();
        }

        private void Validation(string name, string codErp, decimal price)
        {
            DomainValidationException.When(string.IsNullOrEmpty(name), "Nome deve ser informado!");
            DomainValidationException.When(string.IsNullOrEmpty(codErp), "Codigo erp deve ser informado!");
            DomainValidationException.When(price < 0, "Preço deve ser informado!");

            DomainValidationException.When(name.Length > 100, "Nome máximo de 100 caracteres");
            DomainValidationException.When(codErp.Length > 100, "Codigo erp máximo de 100 caracteres");

            Name = name;
            CodErp = codErp;
            Price = price;

        }

    }
}
