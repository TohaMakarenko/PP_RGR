with Ada.Text_IO; use Ada.Text_IO;
with Data;
with Ada.Integer_Text_IO;
with Ada.Synchronous_Task_Control;
use Ada.Integer_Text_IO;
use Ada.Synchronous_Task_Control;

-- a = max(MB*MC + MM)

procedure Main is
   package data1 is new data(2);
   use data1;
begin
   Put_Line("started");
   Put_Line("a = max(MB*MC + MM)");
   Func;
	
end Main;
