const cores = ['#3b82f6', '#10b981', '#f59e0b', '#8b5cf6', '#ec4899', '#06b6d4', '#f97316', '#84cc16', '#6366f1', '#14b8a6'];

function retornarCorAleatoria(): string[] {
    return [...cores].sort(() => Math.random() - 0.5);
}

export interface ItemGraficoPizza {
    name: string;
    value: number;
}

export function criarGraficoPizza(tituloSerie: string, dados: ItemGraficoPizza[]) {
    const mobile = window.innerWidth < 640;
    return {
        color: retornarCorAleatoria(),
        tooltip: {
            trigger: 'item',
            formatter: '{b}: <b>{c}</b> ({d}%)'
        },
        legend: {
            orient: mobile ? 'horizontal' : 'vertical',
            left: mobile ? 'center' : 'auto',
            right: mobile ? 'auto' : '2%',
            bottom: mobile ? '0%' : 'auto',
            top: mobile ? 'auto' : 'center',
            type: 'scroll'
        },
        series: [
            {
                name: tituloSerie,
                type: 'pie',
                radius: '70%',
                avoidLabelOverlap: true,
                itemStyle: {
                    borderRadius: 8,
                    borderColor: '#fff',
                    borderWidth: 2
                },
                label: {
                    show: true,
                    position: 'inside',
                    formatter: '{c} ({d}%)',
                    color: '#ffffff',
                    fontWeight: 'bold',
                    fontSize: 10
                },
                data: dados
            }
        ]
    };
}