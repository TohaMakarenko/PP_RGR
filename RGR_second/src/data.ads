with Ada.Synchronous_Task_Control;
use Ada.Synchronous_Task_Control;

generic
   n: Integer;
package Data is

   type Vector is array(0..n) of Integer;
   type Matrix is array(0..n) of Vector;

   procedure Func;

end Data;
