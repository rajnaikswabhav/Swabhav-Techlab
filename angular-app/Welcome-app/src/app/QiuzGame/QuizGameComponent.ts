import { Component } from '@angular/core';
import { QuizGameService } from "./Service/QuizGameService";


@Component({
    selector : 'ht-quizgame',
    templateUrl : 'QuizGameComponent.html'
})
export class QuizGameComponent {
    quizContainer:string;
    resultContainer:string;

     questionList = [
        {
            question: "Who is Prime Minister of India?",
            answers: {
                a: 'ManmohanSinh',
                b: 'Narendra Modi',
                c: 'Rahul Gandhi',
                d: 'Ram Nath Kovind'
            },
            correctAnswer: 'b'
        },
        {
            question: "Where is India Gate Located?",
            answers: {
                a: 'Ahmedabad',
                b: 'Mumbai',
                c: 'Delhi',
                d: 'Chennai'
            },
            correctAnswer: 'c'
        },
        {
            question: "What is Republic Day of India?",
            answers: {
                a: '26th January',
                b: '15th August',
                c: '5th November',
                d: '3rd March'
            },
            correctAnswer: 'a'
        },
        {
            question: "Who is Richest Man in India?",
            answers: {
                a: 'Aditya Birla',
                b: 'Ratan Tata',
                c: 'Anil Ambani',
                d: 'Mukesh Ambani'
            },
            correctAnswer: 'd'
        },
        {
            question: "Which is largest state in India?",
            answers: {
                a: 'Maharastra',
                b: 'Uttar Pradesh',
                c: 'Rajsthan',
                d: 'Gujarat'
            },
            correctAnswer: 'c'
        },
        {
            question: "Where is the home of Asiatic Lions?",
            answers: {
                a: 'Gujarat',
                b: 'West Bangal',
                c: 'Punjab',
                d: 'TamilNadu'
            },
            correctAnswer: 'a'
        },
        {
            question: "Where is Indian Film Industry Located?",
            answers: {
                a: 'Delhi',
                b: 'Kerala',
                c: 'Bihar',
                d: 'Mumbai'
            },
            correctAnswer: 'd'
        },
        {
            question: "What is 10/2?",
            answers: {
                a: '3',
                b: '5',
                c: '7',
                d: '4'
            },
            correctAnswer: 'b'
        },
        {
            question: "Who is caption of Indian Women Cricket Team?",
            answers: {
                a: 'Manisha Koli',
                b: 'Nilam Chavda',
                c: 'Mitali Raj',
                d: 'Rani Shetty'
            },
            correctAnswer: 'c'
        },
        {
            question: "When India won their first ICC Worldcup?",
            answers: {
                a: '1987',
                b: '1993',
                c: '1978',
                d: '1983'
            },
            correctAnswer: 'd'
        },
    ];

    constructor(private service : QuizGameService){

    }

    BuildQuiz(){
        this.quizContainer = this.service.BuildQuizService(this.questionList,this.quizContainer);
    }

}