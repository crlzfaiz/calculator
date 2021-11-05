First code iteration: ~ 4 hours
Second code iteration: ~ additional 3 hours including summary writing

Assumptions
1. there would be no invalid operation such as "6 + - - 7" or "6 ++" etc.
2. multiplication of side-by-side bracket is not supported i.e. "( 1 + 2 )( 4 - 5 )"


General Information
1. I intend to use 2 Stacks to store both number and operators. Stack is chosen instead of general Array because of the "Last In First out" functionality. For this task's use case, with Stack we dont necessarily need to check for index to keep track of stored items because with proper conditions set, we only need to pop latest 2 numbers as well as 1 operator to perform the required calculation in the correct order. Also it is more convenient to use pop to get the entry we need and remove it from the Stack than using multiple statements to achieve the same with normal array.

2. Determining the operator and parenthesis precedence is necessary to ensure that the calculation is done in the correct order.


Flow
1. First task is to split the string into based on space (assume input string alway valid).

2. Create 2 Stacks to store numbers and operators in from the input array. 

3. For each entry in the inputArray, they will be subjected to the following:
    a. Check if entry value is a number. If it is, store in number Stack
    b. If the entry is an operator, store the entry in the operator Stack or perform calculation/iteration based on the certain condition and/or operator precedence.

4. The arithmetic operator is determined to be "* /" has higher precedence than "+ -". Therefore when performing calculation, "* /" will be executed first.

5. For calculation,they are executed in based on 3 sets of condition 
    a. when right parenthesis is found, the logic will calculate for all the operations available within the enclosing parenthesis. 
    b. when operatorStack.Count != 0 and the current operator entry has lower precedence than operator in Stack. 
    c. What is leftover after at the end when there is no more entry satisfying condition a and/or b. This will complete the rest of the calculation if necessary. The final output in numberStack is popped then returned to the caller.


Additional
1. general error handling so that program does not crash when an error is found
2. exponentiation / power operator is supported