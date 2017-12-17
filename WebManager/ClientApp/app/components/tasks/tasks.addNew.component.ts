import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Router } from '@angular/router';


@Component({
    selector: 'tasks',
    templateUrl: './tasks.addNew.component.html'
})
export class TasksAddNewComponent {
    public task: Task;
    public groups: Group[];

    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string, private router: Router) {
        this.task = new Task();
        http.get(baseUrl + 'api/Groups/GetCurrentUsersGroups').subscribe(result => {
            this.groups = result.json() as Group[];
        }, error => {
            console.error(error);
        });
    }

    public submit() {
        this.http.post(this.baseUrl + 'api/Tasks/AddTask', this.task).subscribe(result => {
            if (result) {
                this.router.navigate(['/tasks']);
            }
        }, error => {
            console.error(error);
        });
    }
}

class Task {
    dueDate: Date;
    title: string;
    details: string;
    groupId: number;
}

interface Group {
    id: number;
    name: string;
}
