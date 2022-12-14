import { Component, OnInit } from '@angular/core';
import { MovieService } from '../movie.service';
import { Movie } from '../movie';
import { ResponseObject } from 'src/app/response';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/authentication/auth.service';


@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit {

  movies: Movie[];

  constructor(public movieService: MovieService,public authService: AuthService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.movieService.getAll().subscribe((data: ResponseObject)=>{
      this.movies = data.payload;
      console.log(this.movies);
    })
  }

  deleteMovie(id:number){
    this.movieService.delete(id).subscribe(res => {
         this.movies = this.movies.filter(item => item.id !== id);
         this.toastr.success('Movie deleted successfully!');
    })
  }

  isAdminUser() {
    return this.authService.isAdmin();
  }

}
