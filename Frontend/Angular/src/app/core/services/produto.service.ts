import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface ProdutoListaResposta {
    idProduto: number;
    idProdutoTipo: number;
    idProdutoSituacao: number;
    idProdutoFabricante: number;
    idProdutoMedida: number;
    nmProduto: string;
    dsProduto: string;
    inProdutoCor: number;
    qtProduto: number;
    lkProdutoImagem: string;
    txAnotacao: string;
}
export interface ProdutoCadastroRequisicao {
    idProdutoTipo: number;
    idProdutoSituacao: number;
    idProdutoFabricante: number;
    idProdutoMedida: number;
    nmProduto: string;
    dsProduto: string;
    inProdutoCor: number;
    lkProdutoImagem: string;
    txAnotacao: string;
    arquivo: File;
}
export interface ProdutoCadastroResposta {
    idProduto: number;
    idProdutoTipo: number;
    idProdutoSituacao: number;
    idProdutoFabricante: number;
    idProdutoMedida: number;
    nmProduto: string;
    dsProduto: string;
    inProdutoCor: number;
    qtProduto: number;
    lkProdutoImagem: string;
    txAnotacao: string;
}
export interface ProdutoAtualizacaoRequisicao {
    idProduto: number;
    idProdutoTipo: number;
    idProdutoSituacao: number;
    idProdutoFabricante: number;
    idProdutoMedida: number;
    nmProduto: string;
    dsProduto: string;
    inProdutoCor: number;
    lkProdutoImagem: string;
    txAnotacao: string;
    arquivo: File;
}
export interface ProdutoExclusaoRequisicao {
    idProduto: number;
}

@Injectable({ providedIn: 'root' })
export class ProdutoService {
    private readonly http = inject(HttpClient);
    private readonly apiUrl = `${environment.apiUrl}/produtos`;

    listar(): Observable<ProdutoListaResposta[]> {
        return this.http.get<ProdutoListaResposta[]>(this.apiUrl);
    }

    cadastrar(dados: ProdutoCadastroRequisicao): Observable<ProdutoCadastroResposta> {
        const formulario = new FormData();

        formulario.append('idProdutoTipo', dados.idProdutoTipo.toString());
        formulario.append('idProdutoSituacao', dados.idProdutoSituacao.toString());
        formulario.append('idProdutoFabricante', dados.idProdutoFabricante.toString());
        if (dados.idProdutoMedida !== null) formulario.append('idProdutoMedida', dados.idProdutoMedida.toString());
        formulario.append('nmProduto', dados.nmProduto);
        if (dados.dsProduto !== null) formulario.append('dsProduto', dados.dsProduto);
        formulario.append('inProdutoCor', dados.inProdutoCor?.toString());
        formulario.append('lkProdutoImagem', dados.lkProdutoImagem);
        if (dados.txAnotacao !== null) formulario.append('txAnotacao', dados.txAnotacao);
        if (dados.arquivo) formulario.append('arquivo', dados.arquivo, dados.arquivo.name);

        return this.http.post<ProdutoCadastroResposta>(this.apiUrl, formulario);
    }

    atualizar(dados: ProdutoAtualizacaoRequisicao): Observable<void> {
        const formulario = new FormData();

        formulario.append('idProduto', dados.idProduto.toString());
        formulario.append('idProdutoTipo', dados.idProdutoTipo.toString());
        formulario.append('idProdutoSituacao', dados.idProdutoSituacao.toString());
        formulario.append('idProdutoFabricante', dados.idProdutoFabricante.toString());
        if (dados.idProdutoMedida !== null) formulario.append('idProdutoMedida', dados.idProdutoMedida.toString());
        formulario.append('nmProduto', dados.nmProduto);
        if (dados.dsProduto !== null) formulario.append('dsProduto', dados.dsProduto);
        formulario.append('inProdutoCor', dados.inProdutoCor.toString());
        if (dados.lkProdutoImagem !== null) formulario.append('lkProdutoImagem', dados.lkProdutoImagem.toString());
        if (dados.txAnotacao !== null) formulario.append('txAnotacao', dados.txAnotacao.toString());
        if (dados.arquivo) formulario.append('arquivo', dados.arquivo, dados.arquivo.name);

        return this.http.put<void>(this.apiUrl, formulario);
    }

    excluir(idProduto: number): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/${idProduto}`);
    }
}