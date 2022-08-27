import { Component, OnInit } from '@angular/core';
import { MovieService } from '../movie.service';
import { UserService } from 'src/app/users/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, Validators} from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Booking } from 'src/app/booking/booking';
import { ResponseObject } from 'src/app/response';
import { Movie, Sample } from '../movie';
import { UserResponse } from 'src/app/users/user.model';
import { BookingService } from 'src/app/booking/booking.service';


@Component({
  selector: 'app-booking',
  templateUrl: './booking.component.html',
  styleUrls: ['./booking.component.css']
})
export class BookingComponent implements OnInit {

  id: number;
  form:FormGroup;
  movieName:string;
  userName:string;
  movie:Movie;
  userId:number;
  price:number;
  n:number=0;

  constructor(
    public movieService: MovieService,
    public userService: UserService,
    public bookingService: BookingService,
    private router: Router,
    private route: ActivatedRoute,
    private toastr: ToastrService
  ) { }
 
  ngOnInit(): void {
    this.userId = Number(localStorage.getItem('id'));
    this.userService.search(this.userId).subscribe((data: UserResponse)=>{
      this.userName=data.payload.name;
    });
    this.id = this.route.snapshot.params['movieId'];
    this.movieService.find(this.id).subscribe((data: ResponseObject)=>{
      this.movie = data.payload;
      this.price=this.movie.price;
    this.form = new FormGroup({
      movieId: new FormControl(this.movie.id, Validators.required),
      userId: new FormControl(this.userId, Validators.required),
      noOfSeats: new FormControl('', Validators.required),
      totalCost: new FormControl('', Validators.required)
    })
  });
  }

  get f(){
    return this.form.controls;
  }

  submit(){
    this.form.get('totalCost')?.setValue(this.price*this.form.get('noOfSeats')?.value);
    this.bookingService.create(this.form.value).subscribe(res => {
      this.toastr.success('Movie Booking successfully!');
      this.router.navigate(['booking/index']);
    })
  }
}
