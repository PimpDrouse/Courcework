   M S B A M L   `   `   `  ����E  ACourceWork, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullW0clr-namespace:CourceWork.ViewModel.RoleViewModel"CourceWork.ViewModel.RoleViewModel  5   	   -    'CourceWork.View.UserWindow.DoctorWindow   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Blocal0clr-namespace:CourceWork.ViewModel.RoleViewModel     5   	   -   5        $��	docWindow��5   	     � Title$  DoctorWindow��5	   	   $	��450��6   $	��800��6+    � DataContext 5      8   2CourceWork.ViewModel.RoleViewModel.DoctorViewModel  5   
   .��5      � .+�5   
   ��  � 	Resources 5       �� 
TargetType �� Property �� Value��%V   '��       �� 5      " ��6   .�5      ��  � ��$ Center=�6<   5      5      .@�5      �� $��Appointments��6   .��5      �� .+�5      &   System.Windows.Controls.DataGrid  $��Stretch=�6      AutoGenerateColumns$ False��6k   $��Italic$�6�   $	��340��6�    �� SelectedItem 6=   �� *SelectedApppointment+��6�   �� *Appointments+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$	 Patient��61   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6B   �� *patientname+  5      $	 Cabinet��61   
 6B   �� *	cabinet+  5      $	 Med pol��61   
 6B   �� *medpol+  5      $
	 Time��61   
 6?   �� *timee+  5      $
	 Date��61   
 6?   �� *datee+5!      �� 5#      $��Cancel appointment��6   $
��0,20q�6`   ��6:   �� *CancelAppointment+6`   5$      5%      �� 5'      $��Settings��6   .��5(      �� .+�5)      �� $��Center=�6    $��
Enter data��6=   $��	0,40,0,30q�6O   $��20(�6b   �� 5+      $��Center=�6    $��Lastname��6=   �� 5,       �� TextAlignment$ Center=�6A   $��0,0,0,20q�6X   ��6   �� *PatientData.Lname+6X   �� 5.      $��Center=�6    $��	Firstname��6=   �� 5/      $ Center=�6A   $��0,0,0,20q�6X   ��6   �� *PatientData.Fname+6X   �� 51      $��Center=�6    $��Surname��6=   �� 52      $ Center=�6A   $��0,0,0,20q�6X   ��6   �� *PatientData.Sname+6X   �� 54      $��Center=�6    $��Hospital��6=   �� 55       �� Text$ -- Select Role --��6_    �� 
IsEditable$
 True��6x    �� 
IsReadOnly$
 True��6�   $��Center=�6�   $��0,0,0,20q�6�   ��6   �� *	Hospitals+ 6A   �� *Item+6�   �� 57      $��Center=�6    $��MedicalPolicy��6=   �� 58      $ Center=�6I   $��0,0,0,20q�6`   ��6   �� *PatientData.MedicalPolicy+6`   �� 5:      $
��Save��6   ��6,   �� *
SaveData+��6I   ��  �� ElementName$ firstEnterPat��5<      5=      �� 5?      $��Right=�6   ��5@      �� 5A      $��Right=�6   $��Log out��69   ��6K   �� *LogOutButton+��6l   �� $ 	docWindow��5C      5D      5E      5F      