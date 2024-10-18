import { HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { AuthModel } from "./auth-model";

@Injectable({
    providedIn: 'root'
})
export class AuthService {

    constructor(private router: Router) { }

    logout() {
        localStorage.removeItem('currentUser');
        this.router.navigateByUrl('/login');
    }

    setItem(key: string, value: any): void {
        localStorage.setItem(key, JSON.stringify(value));
    }


    getItem<T>(key: string) {
        const item = localStorage.getItem(key);
        if (item) {
            return JSON.parse(item) as T;
        }
        return null;
    }

    removeItem(key: string): void {
        localStorage.removeItem(key);
    }

    getToken(): string | null {
        const currentUser = this.getItem<AuthModel>('currentUser');
        return currentUser ? currentUser.token : null;
    }

    getRole(){
        const currentUser = this.getItem<AuthModel>('currentUser');
        return currentUser ? currentUser.role : null;
    }

    isAuthenticated(): boolean {
        const token = this.getToken();
        return token !== null;
    }

    getHeader() {
        const token = this.getToken();
        let httpOptions = {
            headers: new HttpHeaders({
                'Authorization': 'Bearer ' + token
            })
        };
        return httpOptions;
    }

    isLoggedIn(): boolean {
        return !!this.getToken();
      }
}