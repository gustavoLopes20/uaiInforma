import { Component, OnInit } from '@angular/core';
import { AdmComponent } from '../adm.component';
import { Router } from '@angular/router';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-escalas',
  templateUrl: './escalas.component.html',
  styleUrls: ['./escalas.component.scss']
})
export class EscalasComponent implements OnInit {

  public model: any = {};

  public unidades: Array<any> = [];
  public setores: Array<any> = [];
  public consulta: Array<any> = [];


  constructor(
    private router: Router,
    private admComponent: AdmComponent,
    private servidor: ApiService
  ) { }


  async ngOnInit() {
    this.unidades = await this.servidor.chamarApi("api/Unidades", null);
  }

  novoItem() {
    this.router.navigate(['/Admin/Escala/Nova']);
  }

  carregar(op: number = 0) {

    if (op == 1) {
      this.unidades.forEach(unidade => {
        if (unidade.id == this.model.Unidade) {
          this.setores = unidade.setores;
        }
      });
    }
  }

  async consultar() {
    try {
      const response: any = await this.servidor.chamarApi('api/Plantoes/Consulta', this.model);
      this.consulta = JSON.parse(response._body);
    } catch (err) {
      console.log("Erro", err);
    }
  }

  editarItem(escala: any) {
    this.router.navigate(['/Adm/Escala/' + escala.rid]);
  }

}
