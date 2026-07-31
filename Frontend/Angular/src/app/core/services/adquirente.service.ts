import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface AdquirenteListaResposta {
    idAdquirente: number;
    nmAdquirente: string;
    txEndereco: string;
    txAnotacao: string;
}
export interface AdquirenteCadastroRequisicao {
    nmAdquirente: string;
    txEndereco: string;
    txAnotacao: string;
}
export interface AdquirenteCadastroResposta {
    idAdquirente: number;
    nmAdquirente: string;
    txEndereco: string;
    txAnotacao: string;
}
export interface AdquirenteAtualizacaoRequisicao {
    idAdquirente: number;
    nmAdquirente: string;
    txEndereco: string;
    txAnotacao: string;
}
export interface AdquirenteExclusaoRequisicao {
    idAdquirente: number;
}

@Injectable({ providedIn: 'root' })
export class AdquirenteService {
    private readonly http = inject(HttpClient);
    private readonly apiUrl = `${environment.apiUrl}/adquirentes`;

    listar(): Observable<AdquirenteListaResposta[]> {
        return this.http.get<AdquirenteListaResposta[]>(this.apiUrl);
    }

    cadastrar(dados: AdquirenteCadastroRequisicao): Observable<AdquirenteCadastroResposta> {
        return this.http.post<AdquirenteCadastroResposta>(this.apiUrl, dados);
    }

    atualizar(dados: AdquirenteAtualizacaoRequisicao): Observable<void> {
        return this.http.put<void>(this.apiUrl, dados);
    }

    excluir(idAdquirente: number): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/${idAdquirente}`);
    }
}