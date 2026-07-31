import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface UsuarioCadastroRequisicao {
    nmUsuario: string;
    nmLogin: string;
    cdSenha: string;
}
export interface UsuarioCadastroResposta {
    idUsuario: number;
    nmUsuario: string;
    nmLogin: string;
}
export interface UsuarioAtualizacaoRequisicao {
    idUsuario: number;
    nmUsuario: string;
    nmLogin: string;
    cdSenha: string;
}

@Injectable({ providedIn: 'root' })
export class UsuarioService {
    private readonly http = inject(HttpClient);
    private readonly apiUrl = `${environment.apiUrl}/usuarios`;

    cadastrar(dados: UsuarioCadastroRequisicao): Observable<UsuarioCadastroResposta> {
        return this.http.post<UsuarioCadastroResposta>(this.apiUrl, dados);
    }

    atualizar(dados: UsuarioAtualizacaoRequisicao): Observable<void> {
        return this.http.put<void>(this.apiUrl, dados);
    }
}