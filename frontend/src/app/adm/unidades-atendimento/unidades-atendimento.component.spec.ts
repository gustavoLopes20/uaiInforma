import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UnidadesAtendimentoComponent } from './unidades-atendimento.component';

describe('UnidadesAtendimentoComponent', () => {
  let component: UnidadesAtendimentoComponent;
  let fixture: ComponentFixture<UnidadesAtendimentoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UnidadesAtendimentoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UnidadesAtendimentoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
