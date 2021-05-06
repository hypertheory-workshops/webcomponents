import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators'
@Component({
  selector: 'app-temperature',
  templateUrl: './temperature.component.html',
  styleUrls: ['./temperature.component.css']
})
export class TemperatureComponent implements OnInit {

  tempResult$: Observable<TempResponse>;
  constructor(private client: HttpClient) { }

  ngOnInit(): void {
  }

  convert(tempEl: HTMLInputElement, unit: 'F' | 'C'): void {
    // this is abysmal. I just did this for something quick and dirty.
    this.tempResult$ = this.client.get<TempResponse>(`http://localhost:1337/temp/${tempEl.value}/${unit}`)
      .pipe(
        tap(r => console.log(r))
      );
  }
}

interface TempResponse {
  tempInF: string;
  tempInC: string;
}
