import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatCard, MatCardHeader, MatCardTitle, MatCardContent } from '@angular/material/card';
import { RelatorioService, TotaisResposta, ProdutosPorTiposResposta } from '../../core/services/relatorio.service';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { CurrencyPipe } from '@angular/common';

import * as echarts from 'echarts/core';
import { PieChart } from 'echarts/charts';
import { TooltipComponent, LegendComponent } from 'echarts/components';
import { CanvasRenderer } from 'echarts/renderers';
import { NgxEchartsDirective, provideEchartsCore } from 'ngx-echarts';
import { criarGraficoPizza, ItemGraficoPizza } from '../../shared/components/grafico-pizza';

echarts.use([PieChart, TooltipComponent, LegendComponent, CanvasRenderer]);

@Component({
    selector: 'app-dashboard',
    imports: [MatIconModule, MatButtonModule, MatCard, MatCardHeader, MatCardTitle, MatCardContent, MatProgressBarModule, CurrencyPipe, NgxEchartsDirective],
    providers: [ provideEchartsCore({ echarts }) ],
    templateUrl: './dashboard.component.html',
    styleUrl: './dashboard.component.css'
})

export class DashboardComponent implements OnInit {
    private readonly relatorioService = inject(RelatorioService);
    hoje = signal<Date>(new Date());
    totais = signal<TotaisResposta | null>(null);
    abrindo = signal<boolean>(false);

    graficoProdutosTipos = signal<any>(criarGraficoPizza('Tipos', []));
    graficoProdutosFabricantes = signal<any>(criarGraficoPizza('Fabricantes', []));
    graficoProdutosFornecedores = signal<any>(criarGraficoPizza('Fornecedores', []));
    graficoProdutosCores = signal<any>(criarGraficoPizza('Cores', []));

    ngOnInit(): void {
        this.carregarRelatorios();
    }

    carregarRelatorios() {
        this.abrindo.set(true);
        this.relatorioService.listarTotais().subscribe({
            next: (dados) => {
                this.totais.set(dados);
            },
            error: (erro) => {
                console.error('Erro ao carregar relatório de totais: ', erro);
            }
        });

        this.relatorioService.listarProdutosPorTipos().subscribe({
            next: (dados) => {
                const dadosFormatados: ItemGraficoPizza[] = dados.map(item => ({ 
                    name: item.nmProdutoTipo,
                    value: item.vlProdutoTipo 
                  }));
                this.graficoProdutosTipos.set(criarGraficoPizza('Tipos', dadosFormatados));
            },
            error: (erro) => {
                console.error('Erro ao carregar relatório de totais: ', erro);
            }
        });

        this.relatorioService.listarProdutosPorFabricantes().subscribe({
            next: (dados) => {
                const dadosFormatados: ItemGraficoPizza[] = dados.map(item => ({ 
                    name: item.nmProdutoFabricante,
                    value: item.vlProdutoFabricante 
                  }));
                this.graficoProdutosFabricantes.set(criarGraficoPizza('Fabricantes', dadosFormatados));
            },
            error: (erro) => {
                console.error('Erro ao carregar relatório de totais: ', erro);
            }
        });

        this.relatorioService.listarProdutosPorFornecedores().subscribe({
            next: (dados) => {
                const dadosFormatados: ItemGraficoPizza[] = dados.map(item => ({ 
                    name: item.nmProdutoFornecedor,
                    value: item.vlProdutoFornecedor 
                  }));
                this.graficoProdutosFornecedores.set(criarGraficoPizza('Fornecedores', dadosFormatados));
            },
            error: (erro) => {
                console.error('Erro ao carregar relatório de totais: ', erro);
            }
        });

        this.relatorioService.listarProdutosPorCores().subscribe({
            next: (dados) => {
                const dadosFormatados: ItemGraficoPizza[] = dados.map(item => ({ 
                    name: item.nmProdutoCor,
                    value: item.vlProdutoCor
                  }));
                this.graficoProdutosCores.set(criarGraficoPizza('Cores', dadosFormatados));
                this.abrindo.set(false);
            },
            error: (erro) => {
                console.error('Erro ao carregar relatório de totais: ', erro);
                this.abrindo.set(false);
            }
        });
    }

    data = computed(() => {
        return this.hoje().toLocaleDateString('pt-BR', {
            day: '2-digit',
            month: 'long',
            year: 'numeric'
        });
    });
}