import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface ProdutoHistoricoListaResposta {
    idProdutoHistorico: number;
    idProduto: number;
    idFornecedor: number;
    idAdquirente: number;
    inProdutoHistoricoTipo: number;
    dtProdutoHistorico: Date;
    qtProdutoHistorico: number;
    vlProdutoHistorico: number;
}
export interface ProdutoHistoricoCadastroRequisicao {
    idProduto: number;
    idFornecedor: number;
    idAdquirente: number;
    inProdutoHistoricoTipo: number;
    dtProdutoHistorico: Date;
    qtProdutoHistorico: number;
    vlProdutoHistorico: number;
}
export interface ProdutoHistoricoCadastroResposta {
    idProdutoHistorico: number;
    idProduto: number;
    idFornecedor: number;
    idAdquirente: number;
    inProdutoHistoricoTipo: number;
    dtProdutoHistorico: Date;
    qtProdutoHistorico: number;
    vlProdutoHistorico: number;
}
export interface ProdutoHistoricoAtualizacaoRequisicao {
    idProdutoHistorico: number;
    idProduto: number;
    idFornecedor: number;
    idAdquirente: number;
    inProdutoHistoricoTipo: number;
    dtProdutoHistorico: Date;
    qtProdutoHistorico: number;
    vlProdutoHistorico: number;
}
export interface ProdutoHistoricoExclusaoRequisicao {
    idProdutoHistorico: number;
}

@Injectable({ providedIn: 'root' })
export class ProdutoHistoricoService {
    private readonly http = inject(HttpClient);
    private readonly apiUrl = `${environment.apiUrl}/produtos/historicos`;

    listar(): Observable<ProdutoHistoricoListaResposta[]> {
        return this.http.get<ProdutoHistoricoListaResposta[]>(this.apiUrl);
    }

    listarPorProduto(idProduto: number): Observable<ProdutoHistoricoListaResposta[]> {
        return this.http.get<ProdutoHistoricoListaResposta[]>(`${this.apiUrl}/${idProduto}`);
    }

    cadastrar(dados: ProdutoHistoricoCadastroRequisicao): Observable<ProdutoHistoricoCadastroResposta> {
        return this.http.post<ProdutoHistoricoCadastroResposta>(this.apiUrl, dados);
    }

    atualizar(dados: ProdutoHistoricoAtualizacaoRequisicao): Observable<void> {
        return this.http.put<void>(this.apiUrl, dados);
    }

    excluir(idProdutoHistorico: number): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/${idProdutoHistorico}`);
    }
}