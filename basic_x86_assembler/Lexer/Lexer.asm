section	.text
    global _start
_start:	  

    call _initialise
    mov eax, 4
    mov ebx, 1
    mov ecx, setcolor
    mov edx, setcolor.len
    int 80h

    mov rax, welcome
    call _print

    mov eax, 4
    mov ebx, 1
    mov ecx, defaultcolor
    mov edx, defaultcolor.len
    int 80h

    xor rax, rax
   ; mov rdi, rax
   ; mov rsi, userpass
   ; mov rdx, rax
   ; add rdx, 0x64
   ; syscall

  mov eax,5
  mov ebx,filename 
  mov ecx,0 
  int 80h 

  mov [descriptor],eax 

  mov eax,3 
  mov ebx,[descriptor] 
  mov ecx,buffer
  mov edx,len 
  int 80h 

  ;mov edx,eax 
  ;mov eax,4 
  ;mov ebx,1
  ;mov ecx,buffer 
  ;int 80h 

  mov eax,6 
  mov ebx,[descriptor] 
  int 80h 

    xor rcx,rcx
    xor rax,rax
    xor ebx,ebx 
    xor eax,eax 

    lea rsi, [buffer]
;EXTREM DE IMPORTANT>>> AICI TIN STRUCTURA DESPRE TOKENURI!!!!

xor r15d,r15d
mov r15d,0
xor r14d,r14d
lea r14d, [token_array]

%define function_starts '1'
%define function_ends '2'
%define identifiers '3'
%define call_functions '4'
%define operators '5'
%define system_functions '6'
%define labels '7'
%define keywords '8'
%define values '9'

		;mov byte[token_array+r15d], function_start
                ;inc r15d
                ;mov eax,4            
	        ;mov ebx,1            
	        ;mov ecx, token_array
	        ;mov edx, 1
	        ;int 80h 

;SFIRSIT DEFINIRE STRUCTURA
    
    startLexer:
;increase index

        xor eax,eax
 	xor r12d,r12d
       
        reset_array:
         
         cmp byte[board+eax], ' '
         je exit_reset
         mov byte[board+eax], 0
         inc eax

        jmp reset_array
        
exit_reset: 

    inc rsi
;check EOF
    dec rsi
    cmp byte[rsi], '|'
    je _exit
    cmp byte[rsi], -1
    je _exit
    inc rsi
;check space
    cmp byte[rsi], ' '
    je startLexer
    cmp byte[rsi], 0xa
    je startLexer
   
     secondLoop:

	cmp byte[rsi], 'g'
        je goto
        
        cmp byte[rsi], '.'
        je Function

	cmp byte[rsi], '='
	je equal_operator 

        cmp byte[rsi], '_'
	je endf  

        cmp byte[rsi], ':'
	je label  

        cmp byte[rsi], '&'
	je call 

        cmp byte[rsi], 'M'
	je system_mul

        cmp byte[rsi], 'S'
	je system_sca

        cmp byte[rsi], 'D'
	je system_div

        cmp byte[rsi], 'A'
	je system_add

        cmp byte[rsi], 'C'
	je system_cmp

        cmp byte[rsi], 'J'
	je JE

	not_system:
        jmp variable
                  

;next iteration
    jmp startLexer
       
 
  
_exit:

   mov	rax, 1
   xor	rbx, rbx
   int	80h

search_tree:
   cmp byte[remember_array+esi], 0
   je search_tree
   mov byte[remember_array+esi], 1
   jmp search_tree

;Note to self. Nu schimba lungimea sau moare codu ocazional. Probabil environment modifica registru CX????
array_buffer db 'M','U','L',0,'D','I','V',0,'S','C','A',0,'A','D','D',0,'g','o','t','o',0,':',0,'_',0,'&',0,'.',0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0, 0 , 1

system_mul:
xor r9b,r9b
xor eax,eax

cmp byte[rsi], 'M'
jne not_system
mov r9b, [rsi]
mov byte[board+eax], r9b

inc rsi
inc eax

cmp byte[rsi], 'U'
jne not_system
mov r9b, [rsi]
mov byte[board+eax], r9b

inc rsi
inc eax

cmp byte[rsi], 'L'
jne not_system
mov r9b, [rsi]
mov byte[board+eax], r9b

inc rsi
inc eax
                

                mov eax,4           
	        mov ebx,1            
	        mov ecx,board   
	        mov edx,board_len   
	        int 80h     

                mov eax,4          
	        mov ebx,1            
	        mov ecx,sfunc  
	        mov edx,sfunclen   
	        int 80h 

	        mov byte[token_array+r15d], system_functions
                inc r15d

              jmp startLexer  

system_cmp:
xor r9b,r9b
xor eax,eax

cmp byte[rsi], 'C'
jne not_system
mov r9b, [rsi]
mov byte[board+eax], r9b

inc rsi
inc eax

cmp byte[rsi], 'M'
jne not_system
mov r9b, [rsi]
mov byte[board+eax], r9b

inc rsi
inc eax

cmp byte[rsi], 'P'
jne not_system
mov r9b, [rsi]
mov byte[board+eax], r9b

inc rsi
inc eax
                

                mov eax,4            
	        mov ebx,1            
	        mov ecx,board   
	        mov edx,board_len  
	        int 80h 

               


                mov eax,4            
	        mov ebx,1            
	        mov ecx,sfunc   
	        mov edx,sfunclen   
	        int 80h 

                mov byte[token_array+r15d], system_functions
                inc r15d

                jmp startLexer  


system_div:
xor r9b,r9b
xor eax,eax

cmp byte[rsi], 'D'
jne not_system
mov r9b, [rsi]
mov byte[board+eax], r9b

inc rsi
inc eax

cmp byte[rsi], 'I'
jne not_system
mov r9b, [rsi]
mov byte[board+eax], r9b

inc rsi
inc eax

cmp byte[rsi], 'V'
jne not_system
mov r9b, [rsi]
mov byte[board+eax], r9b

inc rsi
inc eax
                

                mov eax,4            
	        mov ebx,1          
	        mov ecx,board   
	        mov edx,board_len   
	        int 80h 

               


                mov eax,4           
	        mov ebx,1            
	        mov ecx,sfunc  
	        mov edx,sfunclen   
	        int 80h 

		mov byte[token_array+r15d], system_functions
                inc r15d

                jmp startLexer  

system_add:
xor r9b,r9b
xor eax,eax

cmp byte[rsi], 'A'
jne not_system
mov r9b, [rsi]
mov byte[board+eax], r9b

inc rsi
inc eax

cmp byte[rsi], 'D'
jne not_system
mov r9b, [rsi]
mov byte[board+eax], r9b

inc rsi
inc eax

cmp byte[rsi], 'D'
jne not_system
mov r9b, [rsi]
mov byte[board+eax], r9b

inc rsi
inc eax
                

                mov eax,4            
	        mov ebx,1            
	        mov ecx,board   
	        mov edx,board_len  
	        int 80h 


                mov eax,4            
	        mov ebx,1            
	        mov ecx,sfunc   
	        mov edx,sfunclen   
	        int 80h 

		mov byte[token_array+r15d], system_functions
                inc r15d

                jmp startLexer  


system_sca:
xor r9b,r9b
xor eax,eax

cmp byte[rsi], 'S'
jne not_system
mov r9b, [rsi]
mov byte[board+eax], r9b

inc rsi
inc eax

cmp byte[rsi], 'C'
jne not_system
mov r9b, [rsi]
mov byte[board+eax], r9b

inc rsi
inc eax

cmp byte[rsi], 'A'
jne not_system
mov r9b, [rsi]
mov byte[board+eax], r9b

inc rsi
inc eax
                

                mov eax,4           
	        mov ebx,1            
	        mov ecx,board   
	        mov edx,board_len   
	        int 80h 

                mov eax,4            
	        mov ebx,1           
	        mov ecx,sfunc   
	        mov edx,sfunclen   
	        int 80h 

		mov byte[token_array+r15d], system_functions
                inc r15d

                jmp startLexer  


endf:
           end_function_loop:
       	   cmp byte[rsi], ' '
       	   je end_another_function 
     	   cmp byte[rsi], '|'
     	   je end_another_function 
           cmp byte[rsi], 0xa
     	   je end_another_function

     	   mov r9b, [rsi]
     	   mov byte[board+r12d], r9b 
     	   inc r12d
     	   inc rsi
      	   jmp end_function_loop

end_another_function:
	
	        mov eax,4          
	        mov ebx,1            
	        mov ecx,board     
	        mov edx,board_len   
	        int 80h  

                mov eax,4            
	        mov ebx,1        
	        mov ecx,end  
	        mov edx,endlen   
	        int 80h 

                mov byte[token_array+r15d], function_ends
                inc r15d

                jmp startLexer   

label:
           label_loop:
       	   cmp byte[rsi], ' '
       	   je end_another_label
     	   cmp byte[rsi], '|'
     	   je end_another_label 
           cmp byte[rsi], 0xa
     	   je end_another_label
           
     	   mov r9b, [rsi]
     	   mov byte[board+r12d], r9b 
     	   inc r12d
     	   inc rsi
      	   jmp label_loop

end_another_label:
	
	        mov eax,4           
	        mov ebx,1            
	        mov ecx,board     
	        mov edx,board_len   
	        int 80h  

                mov eax,4            
	        mov ebx,1            
	        mov ecx,labelp   
	        mov edx,labellen   
	        int 80h 

		mov byte[token_array+r15d], labels
                inc r15d

                jmp startLexer   

call:
           call_loop:
       	   cmp byte[rsi], ' '
       	   je end_another_call
     	   cmp byte[rsi], '|'
     	   je end_another_call 
           cmp byte[rsi], 0xa
     	   je end_another_call
           
     	   mov r9b, [rsi]
     	   mov byte[board+r12d], r9b 
     	   inc r12d
     	   inc rsi
      	   jmp call_loop

end_another_call:
	
	        mov eax,4          
	        mov ebx,1            
	        mov ecx,board    
	        mov edx,board_len   
	        int 80h  

                mov eax,4           
	        mov ebx,1           
	        mov ecx,callp  
	        mov edx,calllen   
	        int 80h 

		mov byte[token_array+r15d], call_functions
                inc r15d

                jmp startLexer   

goto:
        goto_Loop:
		xor eax,eax
		mov byte[board+eax], 'g' 

                inc eax
                inc rsi
                mov r12d,eax
                
                cmp byte[rsi], 'o'
                jne variable
                mov byte[board+eax], 'o' 

                inc eax
                inc rsi
		mov r12d,eax

		cmp byte[rsi], 't'
                jne variable
                mov byte[board+eax], 't'

                inc eax 
                inc rsi
		mov r12d,eax

		cmp byte[rsi], 'o'
                jne variable
	        mov byte[board+eax], 'o'

                inc eax
                mov r12d,eax

		mov r8d,eax
		xor eax,eax
                mov eax,4            
	        mov ebx,1           
	        mov ecx,board     
	        mov edx,board_len   
	        int 80h               

                inc rsi
                mov r12d,eax
                
                mov eax,4            
	        mov ebx,1            
	        mov ecx,keyword     
	        mov edx,keywordlen  
	        int 80h              
               
		mov byte[token_array+r15d], keywords
                inc r15d

                jmp startLexer

JE:
        JE_Loop:
		xor eax,eax
		mov byte[board+eax], 'J' 

                inc eax
                inc rsi
                mov r12d,eax
                
                cmp byte[rsi], 'E'
                jne variable
                mov byte[board+eax], 'E' 


                inc eax
                mov r12d,eax
           

                inc rsi

                mov eax,4            
	        mov ebx,1            
	        mov ecx,board
	        mov edx,board_len  
	        int 80h 
                
                mov eax,4            
	        mov ebx,1            
	        mov ecx,sfunc 
	        mov edx,sfunclen  
	        int 80h              
               
		mov byte[token_array+r15d], system_functions 	
                inc r15d

                jmp startLexer

variable:

	variable_loop:
        cmp byte[rsi], ' '
        je truly_variable 
        cmp byte[rsi], '|'
        je truly_variable 
        cmp byte[rsi], 0xa
        je truly_variable 
        cmp byte[rsi], '='
        je truly_variable_equal

        mov r9b, [rsi]
        mov byte[board+r12d], r9b 
        inc r12d
        inc rsi

        jmp variable_loop

equal_operator:
		
		
		xor r12d,r12d
                mov eax,4            
	        mov ebx,1            
	        mov ecx,equal     
	        mov edx,equallen  
	        int 80h  

                mov eax,4           
	        mov ebx,1            
	        mov ecx,arithmeticop    
	        mov edx,arithmeticoplen  
	        int 80h

		mov byte[token_array+r15d], operators
                inc r15d

 		remove_white_space:
                inc rsi
		cmp byte[rsi], ' '
                je remove_white_space 
                cmp byte[rsi], 0xa
                je remove_white_space
                 	
                               

		value_loop:
                  
		  cmp byte[rsi], ' '
                  je exit_value_loop
                  cmp byte[rsi], '|'
                  je exit_value_loop 
                  cmp byte[rsi], 0xa
                  je exit_value_loop 
		
                mov r9b, [rsi]
		mov byte[board+r12d], r9b 
		inc r12d
                inc rsi
		jmp value_loop

                  exit_value_loop:
			
		mov eax,4            
	        mov ebx,1         
	        mov ecx,board     
	        mov edx,board_len   
	        int 80h  

                mov eax,4            
	        mov ebx,1           
	        mov ecx,value    
	        mov edx,valuelen   
	        int 80h 

		mov byte[token_array+r15d], values
                inc r15d


cmp byte[rsi], '-'
je arithmetic_operator
cmp byte[rsi], '+'
je arithmetic_operator
cmp byte[rsi], '*'
je arithmetic_operator
cmp byte[rsi], '/'
je arithmetic_operator

                jmp startLexer

arithmetic_operator:

        mov r9b, [rsi]
        mov byte[board+r12d], r9b 

                mov eax,4            
	        mov ebx,1           
	        mov ecx,[board+r12d]     
	        mov edx,1   
	        int 80h  

                mov eax,4            
	        mov ebx,1         
	        mov ecx,arithmeticop   
	        mov edx,arithmeticoplen  
	        int 80h 

		mov byte[token_array+r15d], operators
                inc r15d
        

truly_variable_equal:
 
               dec rsi
               jmp truly_variable
	
truly_variable:

                mov eax,4            
	        mov ebx,1            
	        mov ecx,board     
	        mov edx,board_len   
	        int 80h 

                mov eax,4            
	        mov ebx,1            
	        mov ecx,indentifier    
	        mov edx,indentifierlen   
	        int 80h 

		mov byte[token_array+r15d], identifiers
                inc r15d

                jmp startLexer

Function:
		Function_Loop:
		xor eax,eax
		mov byte[board+eax], '.' 

                inc eax
                inc rsi
                mov r12d,eax
                
                cmp byte[rsi], 'M'
                jne just_function
                mov byte[board+eax], 'M' 

                inc eax
                inc rsi
		mov r12d,eax

		cmp byte[rsi], 'a'
                jne just_function
                mov byte[board+eax], 'a'

                inc eax 
                inc rsi
		mov r12d,eax

		cmp byte[rsi], 'i'
                jne just_function
	        mov byte[board+eax], 'i'

                inc eax
                inc rsi
		mov r12d,eax

		cmp byte[rsi], 'n'
                jne just_function
	        mov byte[board+eax], 'n'

                inc eax
                mov r12d,eax

		mov r8d,eax
		xor eax,eax
                mov eax,4            
	        mov ebx,1            
	        mov ecx,board       
	        mov edx,board_len    
	        int 80h               

                inc rsi
                mov r12d,eax
                
                mov eax,4           
	        mov ebx,1            
	        mov ecx,main        
	        mov edx,mainlen      
	        int 80h              
                
		mov byte[token_array+r15d], function_starts
                inc r15d

                jmp startLexer

just_function:

        function_loop:
       	   cmp byte[rsi], ' '
       	   je another_function 
     	   cmp byte[rsi], '|'
     	   je another_function 
           cmp byte[rsi], 0xa
     	   je another_function 
     	   mov r9b, [rsi]
     	   mov byte[board+r12d], r9b 
     	   inc r12d
     	   inc rsi
      	   jmp function_loop

another_function:
	
	        mov eax,4            
	        mov ebx,1            
	        mov ecx,board    
	        mov edx,board_len   
	        int 80h  

                mov eax,4            
	        mov ebx,1            
	        mov ecx,function    
	        mov edx,functionlen   
	        int 80h 

		mov byte[token_array+r15d], function_starts
                inc r15d

                jmp startLexer   

_print:

    push rax
    mov rbx, 0

_printLoop:

    inc rax
    inc rbx
    mov cl, [rax]
    cmp cl, 0
    jne _printLoop
    mov rax, 1
    mov rdi, 1
    pop rsi
    mov rdx, rbx
    syscall
    ret
 	
_initialise:
    mov eax, 4                         
    mov ebx, 1                          
    mov ecx, ClearTerm                  
    mov edx, CLEARLEN                  
    int 80h
    ret
;Lexer prints

_deadpoint:

section .bss
buffer resb 1024
  len equ 1024
  file_descriptor resd 1
   descriptor resb 4
section	.data
filename db "r.txt", 0
   userpass times 100  db 0 ,0xa,0
   welcome db "enter text to the lexer: " ,0xa,0
setcolor db 1Bh, '[32;40m', 0  ; cyan on black
   .len equ $ - setcolor
   defaultcolor db 1Bh, '[37;40m', 0  ; white on black
   .len equ $ - defaultcolor
ClearTerm: db   27,"[H",27,"[2J"    ; <ESC> [H <ESC> [2J
   CLEARLEN   equ  $-ClearTerm         
;Lexer values declared here
        newline db "", 0xa ,0
        whitespace db " ", 0xa,0
        main db "= {Main function}", 0xa
        mainlen:  equ $-main
        end db "= {EndOfFunction}", 0xa
        endlen:  equ $-end
        sfunc db "= {SystemFunction}", 0xa
        sfunclen:  equ $-sfunc
        equal db "= ", 
        equallen:  equ $-equal
        callp db "= {FunctionCall}", 0xa
        calllen:  equ $-callp
        arithmeticop db "= {Operator}", 0xa
        arithmeticoplen:  equ $-arithmeticop
        keyword db "= {Keywords}", 0xa
        keywordlen:  equ $-keyword
        function db "= {Function}", 0xa
        functionlen:  equ $-function
        labelp db "= {Label}", 0xa
        labellen:  equ $-labelp
        indentifier db "= {Indentifier}", 0xa
        indentifierlen:  equ $-indentifier
	value db "= {Value}", 0xa
        valuelen:  equ $-value        
	board           db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, 0, 0, 0,0 , " " 
        board_len       equ     $ - board
        remember_array           db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, 0, 0, 0,0 , " "
        remember_array_len       equ     $ - remember_array
        token_array           times 100 db 0
        token_array_len       equ     $ - token_array
