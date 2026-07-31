import { Routes } from '@angular/router';
import { autenticacaoGuard } from './core/guards/autenticacao.guard';

export const routes: Routes = [
    { path: 'login', loadComponent: () => import('./pages/login/login.component').then(m => m.LoginComponent) },
    { path: 'usuario/cadastro', loadComponent: () => import('./pages/usuario/cadastro/cadastro.component').then(m => m.UsuarioCadastroComponent) },
    { path: '', loadComponent: () => import('./layouts/layout-principal/layout-principal.component').then(m => m.LayoutPrincipalComponent), canActivate: [autenticacaoGuard],
        children: [
            { path: 'dashboard', loadComponent: () => import('./pages/dashboard/dashboard.component').then(m => m.DashboardComponent) },
            { path: 'historico', loadComponent: () => import('./pages/produto-historico/produto-historico.component').then(m => m.ProdutoHistoricoComponent) },
            { path: 'produto', loadComponent: () => import('./pages/produto/produto.component').then(m => m.ProdutoComponent) },
            { path: 'produto/tipo', loadComponent: () => import('./pages/produto-tipo/produto-tipo.component').then(m => m.ProdutoTipoComponent) },
            { path: 'produto/situacao', loadComponent: () => import('./pages/produto-situacao/produto-situacao.component').then(m => m.ProdutoSituacaoComponent) },
            { path: 'produto/fabricante', loadComponent: () => import('./pages/produto-fabricante/produto-fabricante.component').then(m => m.ProdutoFabricanteComponent) },
            { path: 'adquirente', loadComponent: () => import('./pages/adquirente/adquirente.component').then(m => m.AdquirenteComponent) },
            { path: 'fornecedor', loadComponent: () => import('./pages/fornecedor/fornecedor.component').then(m => m.FornecedorComponent) },
            { path: 'usuario/perfil', loadComponent: () => import('./pages/usuario/perfil/perfil.component').then(m => m.UsuarioPerfilComponent) },
            { path: '', redirectTo: 'dashboard',  pathMatch: 'full' }
        ]
    },
    { path: '**', redirectTo: 'login', pathMatch: 'full' }
];