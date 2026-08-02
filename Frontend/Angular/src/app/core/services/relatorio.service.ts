import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface TotaisResposta {
    qtTotalProduto: number,
    vlTotalProduto: number,
    qtTotalComprado: number,
    vlTotalComprado: number,
    qtTotalVendido: number,
    vlTotalVendido: number
}
export interface ProdutosPorTiposResposta {
    nmProdutoTipo: string;
    vlProdutoTipo: number;
}
export interface ProdutosPorFabricantesResposta {
    nmProdutoFabricante: string;
    vlProdutoFabricante: number;
}
export interface ProdutosPorFornecedoresResposta {
    nmProdutoFornecedor: string;
    vlProdutoFornecedor: number;
}
export interface ProdutosPorCoresResposta {
    nmProdutoCor: string;
    vlProdutoCor: number;
}

@Injectable({ providedIn: 'root' })
export class RelatorioService {
    private readonly http = inject(HttpClient);
    private readonly apiUrl = `${environment.apiUrl}/relatorios`;

    listarTotais(): Observable<TotaisResposta> {
        return this.http.get<TotaisResposta>(`${this.apiUrl}/totais`);
    }

    listarProdutosPorTipos(): Observable<ProdutosPorTiposResposta[]> {
        return this.http.get<ProdutosPorTiposResposta[]>(`${this.apiUrl}/produtos/tipos`);
    }

    listarProdutosPorFabricantes(): Observable<ProdutosPorFabricantesResposta[]> {
        return this.http.get<ProdutosPorFabricantesResposta[]>(`${this.apiUrl}/produtos/fabricantes`);
    }

    listarProdutosPorFornecedores(): Observable<ProdutosPorFornecedoresResposta[]> {
        return this.http.get<ProdutosPorFornecedoresResposta[]>(`${this.apiUrl}/produtos/fornecedores`);
    }

    listarProdutosPorCores(): Observable<ProdutosPorCoresResposta[]> {
        return this.http.get<ProdutosPorCoresResposta[]>(`${this.apiUrl}/produtos/cores`);
    }
}