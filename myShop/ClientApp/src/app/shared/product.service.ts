import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IProduct } from '../Interfaces/IProduct';
import { ICartProduct } from '../Interfaces/ICartProduct';
import { Observable, BehaviorSubject } from 'rxjs';
import { IProductColour } from '../Interfaces/IProductColour';
import { IProductSize } from '../Interfaces/IProductSize';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  cartProduct: ICartProduct[] = [];
  private messageProduct:  BehaviorSubject<ICartProduct[]> = new BehaviorSubject<ICartProduct[]>(this.cartProduct);
  getMessage = this.messageProduct.asObservable();
  private baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') _baseUrl: string)
  {
    this.baseUrl = _baseUrl;
  }

  getAll() {
    return this.http.get<IProduct[]>(this.baseUrl + 'Product/GetAll');
  }

  createAddress() {
    var r = this.http.post<any>(this.baseUrl + 'Checkout/CreateAddress/', null);
    return r;
  }

  getCountries() {
    var r = this.http.get<any>(this.baseUrl + 'Checkout/GetCountries');
    return r;
  }

  getAllSizes() {
    return this.http.get<IProductSize[]>(this.baseUrl + 'Product/GetAllSizes');
  }

  getAllColours() {
    return this.http.get<IProductColour[]>(this.baseUrl + 'Product/GetAllColours');
  }
  //public getMessage(): ICartProduct {
  //  return this.messageProduct.asObservable();
  //}

  setMessage(cartItems: ICartProduct[] ) {
    this.messageProduct.next(cartItems);
  }
}
