import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';
import { AuthService } from '../services/auth.service';
import { IComponent, DATA_SOURCE_COMPONENTS } from '../data/dataModels';

@Component({
  selector: 'app-adm',
  templateUrl: './adm.component.html',
  styleUrls: ['./adm.component.scss']
})
export class AdmComponent implements OnInit {

  public components: Array<IComponent> = DATA_SOURCE_COMPONENTS;
  public unidades: Array<any> = [];
  public usuario: any = {};
  public requestUnidades: Promise<any>;
  public carregar: boolean = true;

  constructor(private servidor: ApiService, public session: AuthService) { }

  async ngOnInit() {
    //verificando usuario
    this.usuario = await this.session.authenticate();

    //recebendo unidades
    this.unidades = await this.servidor.chamarApi("api/Unidades", null);
  }


}
