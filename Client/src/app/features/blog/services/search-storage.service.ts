import {Injectable} from '@angular/core';

@Injectable({providedIn: 'root'})
export class SearchStorageService {

  private searchValueKey = "searchValueKey";
  private searchResultKey = "searchResultKey";

  constructor() {
  }

  public getSearchValue(): string | null {
    return localStorage.getItem(this.searchValueKey);
  }

  public getSearchResult(): string | null {
    return localStorage.getItem(this.searchResultKey);
  }

  public setSearchValue(value: string): void {
    localStorage.setItem(this.searchValueKey, value);
  }

  public setSearchResult(value: string): void {
    localStorage.setItem(this.searchResultKey, value);
  }

  public reset(): void {
    localStorage.removeItem(this.searchValueKey);
    localStorage.removeItem(this.searchResultKey);
  }
}
