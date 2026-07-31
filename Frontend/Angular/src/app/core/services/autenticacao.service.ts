import { Injectable, inject, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface Usuario {
    idUsuario: number;
    nmUsuario: string;
    nmLogin: string;
    cdToken: string;
    txMensagem: string;
}

@Injectable({ providedIn: 'root' })
export class AutenticacaoService {
    private readonly http = inject(HttpClient);
    private readonly usuarioLogado = signal<Usuario | null>(this.retornarUsuario());
    private readonly apiUrl = `${environment.apiUrl}/autenticacao/login`;
    readonly usuarioDados = this.usuarioLogado.asReadonly();
    autenticado = signal<boolean>(false);

    constructor() {
        this.autenticado.set(this.possuiToken());
    }

    possuiToken(): boolean {
        if (typeof window !== 'undefined' && window.localStorage) return !!localStorage.getItem('usuario_token');
        return false;
    }

    login(nmLogin: string, cdSenha: string): Observable<Usuario> {
        return this.http.post<Usuario>(this.apiUrl, { nmLogin, cdSenha }).pipe(
            tap((resposta) => {
                if (resposta && resposta.cdToken) {
                    localStorage.setItem('usuario_sessao', JSON.stringify(resposta));
                    localStorage.setItem('usuario_token', resposta.cdToken);
                    this.usuarioLogado.set(resposta);
                    this.autenticado.set(true);
                }
            })
        );
    }

    logout(): void {
        if (typeof window !== 'undefined' && window.localStorage) {
            localStorage.removeItem('usuario_sessao');
            localStorage.removeItem('usuario_token');
        }
        this.autenticado.set(false);
        this.usuarioLogado.set(null);
    }

    atualizarUsuario(novosDados: Partial<Usuario>): void {
        const usuarioAtual = this.usuarioLogado();
        if (usuarioAtual) {
            const usuarioAtualizado: Usuario = {...usuarioAtual, ...novosDados};
            if (typeof window !== 'undefined' && window.localStorage) localStorage.setItem('usuario_sessao', JSON.stringify(usuarioAtualizado));
            this.usuarioLogado.set(usuarioAtualizado);
        }
    }

    private retornarUsuario(): Usuario | null {
        if (typeof window !== 'undefined' && window.localStorage) {
            const usuarioJson = localStorage.getItem('usuario_sessao');
            return usuarioJson ? JSON.parse(usuarioJson) : null;
        }
        return null;
    }
}