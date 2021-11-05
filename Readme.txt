First code iteration: ~ 4 hours
Second code iteration: ~ additional 2 hours including summary writing

Assumptions
1. there would not be invalid operation such as "6 + - - 7" or "6 ++" etc.
2. multiplication of side-by-side bracket is not supported i.e. "( 1 + 2 )( 4 - 5 )"

iterate array and determine the following
1. open bracket and corresponding close bracket
2. arithmetic operators and associated numbers
3. create a stack to store and load calculation
4. handle multiplication/division first before addition/subtraction
5. handle calculation in bracket first before anything else
6. error handling for invalid input
7. handle 2 sets of brackets
8. use 2 stacks to store value and operator
9. determine precedence
10. make sure the number are in correct order for calculation as they are removed from the stack

In general
1. I intend to use 2 Stacks to store both number and operators. Stack is used because the "Last In First Out" capability is better than general Array when performing operations where ordering is important.
2. Determining the operator and parenthesis precedence is necessary to ensure that the calculation is done in the correct order.
3. 

Additional features
1. general error handling so that program does not crash when an error is found
2. exponentiation / power operator is supported