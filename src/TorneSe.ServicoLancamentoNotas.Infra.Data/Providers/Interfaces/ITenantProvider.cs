﻿using TorneSe.ServicoLancamentoNotas.Dominio.ValueObjects;

namespace TorneSe.ServicoLancamentoNotas.Infra.Data.Providers.Interfaces;

public interface ITenantProvider
{
    Tenant ObterTenant();
    void AtribuirTenant(Tenant tenant);
    bool ValidarTenant(Tenant tenant);
}
