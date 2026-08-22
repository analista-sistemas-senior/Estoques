import { Component, inject, signal } from '@angular/core';
import { Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { AutenticacaoService } from '../../core/services/autenticacao.service';

@Component({
    selector: 'app-layout-principal',
    imports: [RouterOutlet, RouterLink, RouterLinkActive, MatIconModule],
    templateUrl: './layout-principal.component.html'
})

export class LayoutPrincipalComponent {
    private readonly autenticacaoService = inject(AutenticacaoService);
    private readonly router = inject(Router);
    readonly usuario = this.autenticacaoService.usuarioDados;
    readonly anoAtual = new Date().getFullYear();
    readonly versao = "1.0.1";
    menuAberto = signal<boolean>(false);

    alternarMenu(): void {
        this.menuAberto.update(estado => !estado);
    }

    fecharMenuMobile(): void {
        this.menuAberto.set(false);
    }

    logout(): void {
        this.autenticacaoService.logout();
        this.router.navigate(['/login']);
    }
}