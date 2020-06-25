import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css'],
})
export class EventosComponent implements OnInit {
  Eventos: any = [];
  imagemLargura = 90;
  imagemMargem = 2;
  exibirImagem = true;
  _filtroLista: string;
  get filtroLista(): string {
    return this._filtroLista;
  }
  set filtroLista(value: string) {
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista
      ? this.filtrarEventos(this.filtroLista)
      : this.Eventos;
  }

  eventosFiltrados: any = [];

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.getEventos();
  }

  getEventos() {
    this.http.get('http://localhost:5000/api/eventos/').subscribe(
      (response) => {
        this.Eventos = response;
        this.eventosFiltrados = response;
      },
      (error) => {
        console.log(error);
      }
    );
  }
  alternarImagem() {
    this.exibirImagem = !this.exibirImagem;
  }
  filtrarEventos(filtrarPor: string): any {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.Eventos.filter(
      (evento) => evento.local.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }
}
