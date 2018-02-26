


export class QuizGameService {
    BuildQuizService(questionList,quizContainer){
        let output = [];

        questionList.array.forEach((currentQuestion,questionNumber) => {
            let answers = [];

            for(let letter of currentQuestion.answers){

                answers.push(
                    '<label>'
                    + '<input type="radio" name="question' + questionNumber + '" value="' + letter + '">'
                    + letter + ':'
                    + currentQuestion.answers[letter]
                    + '</lable>'
                );
            }

            output.push(currentQuestion.question,answers.join(''));
        });
        quizContainer = output.join('');
        return quizContainer;
    }
}