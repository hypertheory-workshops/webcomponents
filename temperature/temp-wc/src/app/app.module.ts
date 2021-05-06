import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { TemperatureComponent } from './elements/temperature/temperature.component';
import { Injector } from '@angular/core';
import { createCustomElement } from '@angular/elements';
import { environment } from 'src/environments/environment';
import { DoBootstrap } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
@NgModule({
  declarations: [
    AppComponent,
    TemperatureComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: environment.production ? [] : [AppComponent]
})
export class AppModule implements DoBootstrap {

  constructor(private injector: Injector) {

  }

  ngDoBootstrap() {
    const el = createCustomElement(TemperatureComponent, { injector: this.injector });
    customElements.define('ht-temp', el);
  }
}
