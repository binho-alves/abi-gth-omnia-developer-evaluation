using Ambev.DeveloperEvaluation.Domain.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsCancelled { get; set; }
        public SaleStatus Status { get; set; } = SaleStatus.NotCancelled;
        // Relacionamentos
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = new();
        public Guid BranchId { get; set; }
        public Branch Branch { get; set; } = new();
        public List<SaleItem> Items { get; set; } = new();
    }
}
