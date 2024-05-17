using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings
{
    public class TransacaoMapping : IEntityTypeConfiguration<Transacao>
    {
        public void Configure(EntityTypeBuilder<Transacao> builder)
        {
            builder.ToTable("Transacoes", "dbo");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.ValorTotal)
                .HasColumnType("decimal(18,2)");

            builder.Property(c => c.QrCodePix)
                .HasColumnType("varchar(100)");

            builder.Property(c => c.Status)
                .HasColumnType("varchar(20)")
                .HasConversion<string>();

            // 1 : N => Pagamento : Transacao
            builder.HasOne(c => c.Pagamento)
                .WithMany(c => c.Transacoes);
        }
    }
}
