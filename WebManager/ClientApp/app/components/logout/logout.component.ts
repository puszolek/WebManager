import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'logout',
    templateUrl: './logout.component.html'
})
export class LogoutComponent {
    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string)
    {
    }

    public logout() {
        this.http.get(this.baseUrl + 'api/Logout/Logout').subscribe(result =>
        {
            location.reload();
        }, error => console.error(error));
    };
}
