import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface ProdutoSituacaoListaResposta {
    idProdutoSituacao: number;
    nmProdutoSituacao: string;
}
export interface ProdutoSituacaoCadastroRequisicao {
    nmProdutoSituacao: string;
}
export interface ProdutoSituacaoCadastroResposta {
    idProdutoSituacao: number;
    nmProdutoSituacao: string;
}
export interface ProdutoSituacaoAtualizacaoRequisicao {
    idProdutoSituacao: number;
    nmProdutoSituacao: string;
}
export interface ProdutoSituacaoExclusaoRequisicao {
    idProdutoSituacao: number;
}

@Injectable({ providedIn: 'root' })
export class ProdutoSituacaoService {
    private readonly http = inject(HttpClient);
    private readonly apiUrl = `${environment.apiUrl}/produtos/situacoes`;

    listar(): Observable<ProdutoSituacaoListaResposta[]> {
        return this.http.get<ProdutoSituacaoListaResposta[]>(this.apiUrl);
    }

    cadastrar(dados: ProdutoSituacaoCadastroRequisicao): Observable<ProdutoSituacaoCadastroResposta> {
        return this.http.post<ProdutoSituacaoCadastroResposta>(this.apiUrl, dados);
    }

    atualizar(dados: ProdutoSituacaoAtualizacaoRequisicao): Observable<void> {
        return this.http.put<void>(this.apiUrl, dados);
    }

    excluir(idProdutoSituacao: number): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/${idProdutoSituacao}`);
    }
}