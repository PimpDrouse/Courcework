   M S B A M L   `   `   `  ����E  ACourceWork, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullW0clr-namespace:CourceWork.ViewModel.RoleViewModel"CourceWork.ViewModel.RoleViewModel  5   	   .    (CourceWork.View.UserWindow.PatientWindow   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Blocal0clr-namespace:CourceWork.ViewModel.RoleViewModel     5   	   -   5        $��	patWindow��5   	     � Title$  PatientWindow��5	   	   $	��450��6   $	��800��6,    � DataContext 5
      9   3CourceWork.ViewModel.RoleViewModel.PatientViewModel  5   
   .��5      � .+�5   
   ��  � 	Resources 5       �� 
TargetType �� Property �� Value��%V   '��       �� 5      " ��6   .�5      ��  � ��$ Center=�6<   5      5      .@�5      �� $��My Appointments��6   .��5      �� .+�5      &   System.Windows.Controls.DataGrid  $��Stretch=�6      AutoGenerateColumns$ False��6m   $��Italic$�6�   $	��340��6�    �� SelectedItem 6=   �� *SelectedMyApppointment+��6�   �� *MyAppointments+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$	 Doctor��61   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6A   �� *
doctorname+  5      $	 Cabinet��61   
 6B   �� *	cabinet+  5      $	 Specialisation��61   
 6I   �� *spec+  5      $	 Position��61   
 6C   �� *pos+  5      $
	 Time��61   
 6?   �� *timee+  5      $
	 Date��61   
 6?   �� *datee+5!      �� 5#      $��Cancel appointment��6   $
��0,20q�6`   ��6:   �� *CancelAppointment+6`   5$      5%      �� 5'      $��Appointments��6   .��5(      �� .+�5*        $ False��6Q   $��Italic$�6m   $	��340��6�    6   �� *SelectedFreeApppointment+��6�   �� *FreeAppointments+ 5+        5,      $	 Doctor��61   
 6A   �� *
doctorname+  5-      $	 Cabinet��61   
 6B   �� *	cabinet+  5.      $	 Specialisation��61   
 6I   �� *spec+  5/      $	 Position��61   
 6C   �� *pos+  50      $
	 Time��61   
 6?   �� *timee+  51      $
	 Date��61   
 6?   �� *datee+53      �� 55      $��Apply appointment��6   $
��0,20q�6^   ��69   �� *ApplyAppointment+6^   56      57      �� 59      $��Settings��6   .��5:      �� .+�5;      �� $��Center=�6    $��
Enter data��6=   $��	0,30,0,20q�6O   $��20(�6b   �� 5=      $��Center=�6    $��Lastname��6=   �� 5>       �� TextAlignment$ Center=�6<   $��0,0,0,20q�6S   ��6   �� *MyData.Lname+6S   �� 5@      $��Center=�6    $��	Firstname��6=   �� 5A      $ Center=�6<   $��0,0,0,20q�6S   ��6   �� *MyData.Fname+6S   �� 5C      $��Center=�6    $��Surname��6=   �� 5D      $ Center=�6<   $��0,0,0,20q�6S   ��6   �� *MyData.Sname+6S   �� 5F      $��Center=�6    $��Hospital��6=   �� 5G       �� Text$ -- Select Role --��6_    �� 
IsEditable$
 True��6x    �� 
IsReadOnly$
 True��6�   $��Center=�6�   $��0,0,0,20q�6�   ��6   �� *	Hospitals+ 6A   �� *Item+6�   �� 5I      $��Center=�6    $��MedicalPolicy��6=   �� 5J      $ Center=�6D   $��0,0,0,20q�6[   ��6   �� *MyData.MedicalPolicy+6[   �� 5L      $
��Save��6   ��6,   �� *
SaveData+��6I   ��  �� ElementName$ firstEnterPat��5M      5N      �� 5P      $��Right=�6   ��5Q      �� 5R      $��Right=�6   $��Log out��69   ��6K   �� *LogOutButton+��6l   �� $ 	patWindow��5T      5U      5V      5W      