extern @interrupt void  _stext(void);
extern @interrupt void 	TMI2_ISR(void);   
extern @interrupt void  TMI1_ISR(void);   
extern @interrupt void  TMI0_ISR(void);   
extern @interrupt void  CMP1I_ISR(void);  
extern @interrupt void  LVDINT_ISR(void); 
extern @interrupt void  INT_ISR(void);	  
extern @interrupt void  CMP2I_ISR(void); 	
extern @interrupt void  SWI_ISR(void);	  
extern @interrupt void  main(void);			

void (* const vectab[])() = {	
	TMI2_ISR,
	TMI1_ISR,
	TMI0_ISR,
	CMP1I_ISR,
	LVDINT_ISR,
	INT_ISR,	
	CMP2I_ISR, 		
	SWI_ISR,	  		   
	main			
	};

