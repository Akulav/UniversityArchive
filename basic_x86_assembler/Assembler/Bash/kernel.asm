org 0x7e00
bits 16   

;----------------
;CLEAR THE SCREEN
;----------------

jmp clearScreen

;------------
;START OF CLI
;------------

    WelcomeBash:

;-------------------
;PRINT THE BASH TEXT
;-------------------

    mov ah, 0x0e
    mov al, 'c'
    int 10h
    mov al, 'a'
    int 10h
    mov ah, 0x0e
    mov al, 't'
    int 10h
    mov ah, 0x0e
    mov al, '$'
    int 10h
    mov al, '>'
    int 10h

;-------------------
;RESETS INPUT BUFFER
;-------------------

    xor ax,ax
    xor bx,bx
    xor di, di
    xor si, si

    cleanbuffer:
      cmp byte[userpass + si], 0
      je scanloop
      mov byte[userpass + si], 0
      inc si
      jmp cleanbuffer
 
;---------------------------------
;BEGIN SCANNING ONE CHAR AT A TIME
;---------------------------------
behind_bash:

  cmp di,0
  je scanloop
  jmp move_cursor

move_new_line:

  mov ah,0x02
  dec dh
  mov dl, 80
  int 10h
  jmp move_cursor

backspace:

  mov ah, 0x03
  int 10h

  cmp dl, 0
  je move_new_line
  
  cmp dl, 5d
  je behind_bash

move_cursor:

  mov ah, 0x0e
  mov al, 0x08
  int 10h
  mov al, ' '
  int 10h
  dec dl
  mov ah, 0x02
  int 10h
  dec di
  mov byte[userpass + di], 0
	
scanloop:
    mov ah,0x00
    int 16h
    cmp ah, 0x0e
    je backspace
    cmp ah, 0x1c
    je print

cmp di, 256
je scanloop

    mov byte[userpass+di], al
    inc di 
    mov ah,0x0E
    int 0x10
    jmp scanloop

;------------------------
;PRINT THE SCANNED BUFFER
;------------------------

print:
    mov ah,0x03
    int 10h
    cmp dl, 0
    jz dec_row

    mov ah,0x0E
    mov al, 0x0a
    int 10h

    mov ah,0x0E
    mov al, 0x0a
    int 10h
    xor di,di
    mov al, 0x0d
    int 10h

    backloop:
	cmp byte[userpass+di], 0
        je compare
	mov al, byte[userpass+di]
        inc di
        int 10h
        jmp backloop

;----------------------------------------
;COMPARE THE BUFFER TO A LIST OF COMMANDS
;----------------------------------------
        
    compare:
cmp di,0
je WelcomeBash
mov ah,0x0e
mov al, 0x0a
int 10h
mov al, 0x0d
int 10h
                xor cx,cx
        	xor di,di

;-----------------------------
;CHECK IF THE BUFFER = "Diana"
;-----------------------------

		is_it_diana:
			mov bh, byte[Diana+di]
                        cmp byte[userpass+di], bh
                        jne is_it_me
                        inc di
                        cmp byte[Diana+di], 0
                        jne is_it_diana       	      
			xor di,di

;---------------------------------
;RESPONSE IN CASE BUFFER = "Diana"
;---------------------------------

			print_is_it_diana:
				cmp byte[dianaresponse+di], 0
                                je WelcomeBash        
		       	        mov al, byte[dianaresponse+di]
                      	        int 10h
				inc di
                                jmp print_is_it_diana

;-----------------------------
;CHECK IF THE BUFFER = "user"
;-----------------------------

		is_it_me:
			xor di,di
			is_it_me_loop:
			mov bh, byte[me+di]
                        cmp byte[userpass+di], bh
                        jne is_it_clear
                        inc di
                        cmp byte[me+di], 0
                        jne is_it_me_loop      	      
			xor di,di

;------------------------------------
;RESPONSE IN CASE THE BUFFER = "user"
;------------------------------------

			print_is_it_me:
				cmp byte[meresponse+di], 0
                                je WelcomeBash        
		       	        mov al, byte[meresponse+di]
                      	        int 10h
				inc di
                                jmp print_is_it_me

;-----------------------------
;CHECK IF THE BUFFER = "clear"
;-----------------------------

		is_it_clear:
			xor di,di
			is_it_clear_loop:
			mov bh, byte[clear+di]
                        cmp byte[userpass+di], bh
                        jne is_it_color
                        inc di
                        cmp byte[clear+di], 0
                        jne is_it_clear_loop      	      
			xor di,di

;------------------------------------
;RESPONSE IN CASE THE BUFFER = "clear"
;------------------------------------

			clearScreen:
			mov     al, 02h		
      			mov     ah, 00h		
       		        int     10h	
			jmp WelcomeBash	

;-----------------------------
;CHECK IF THE BUFFER = "color"
;-----------------------------

		is_it_color:
			xor di,di
			is_it_color_loop:
			mov bh, byte[color+di]
                        cmp byte[userpass+di], bh
                        jne WelcomeBash
                        inc di
                        cmp byte[color+di], 0
                        jne is_it_color_loop      	      
			xor di,di

;-------------------------------------
;RESPONSE IN CASE THE BUFFER = "color"
;-------------------------------------

colorScreen:

    mov ah,0x0E
    mov al, 0x0a
    int 10h
    xor di,di
    mov al, 0x0d
    int 10h
    mov ah, 0x0e
    changeColorWarning:
	cmp byte[colorchangeWarning+di], 0
        je colorChangeOption1
	mov al, byte[colorchangeWarning+di]
        inc di
        int 10h
        jmp changeColorWarning

  colorChangeOption1:
  mov ah, 0x00
  int 16h

  mov byte[temp], al
  cmp byte[temp], 'A'
  je RED
  cmp byte[temp], 'E'
  je WelcomeBash
  jmp WelcomeBash
 
  RED:

  mov al, 02h		
  mov ah, 00h		
  int 10h	

  MOV AH, 06h    
  XOR AL, AL   
  XOR CX, CX     
  MOV DX, 184FH  
  MOV BH, 42h    
  int 10h
  jmp WelcomeBash
	
;----------------
;EXIT THE PROGRAM
;----------------

exit: 
hlt		
ret  

dec_row:

	mov ah, 0x02
        dec dh
        mov dl, 80
        int 10h
        jmp print

;-------------
;DECLARED DATA
;-------------
    
msg db "catOS: ",0
userpass times 1000 db 0
exitf db 'exit', 0xa  ,0xd,0
exitlen equ $ - exitf
Diana db "Diana" , 0
dianaresponse db "e cea mai bravo", 0xa,0xd,0
me db "user" , 0
meresponse db "Catalin", 0xa,0xd,0
color db "color", 0
clear db "clear", 0
temp times 5 db 0
colorchangeWarning db "Warning! Color change resets screen", 0xa, 0x0d , "Press 'A' to set to red", 0xa,0x0d, "Press 'E' to exit", 0xa,0xd,0

