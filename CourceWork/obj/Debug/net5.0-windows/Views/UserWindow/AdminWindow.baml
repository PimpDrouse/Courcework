   M S B A M L   `   `   `  ����E  ACourceWork, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullW0clr-namespace:CourceWork.ViewModel.RoleViewModel"CourceWork.ViewModel.RoleViewModel  5   	   ,    &CourceWork.View.UserWindow.AdminWindow   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Blocal0clr-namespace:CourceWork.ViewModel.RoleViewModel     5   	   -   5        $��adminWindow��5   	     � Title$  AdminWindow��5	   	   $	��450��6   $	��800��6*    � DataContext 5      7  1CourceWork.ViewModel.RoleViewModel.AdminViewModel  5   
   .��5      � .+�5   
   �� $��Stretch=�6    � 	Resources 5       �� 
TargetType �� Property �� Value��%V   '��       �� 5      " ��6   .�5      ��  � ��$ Center=�6<   5      5      .@�5      �� $��	User list��6   $��Stretch=�6)   .��5      ��  �� Orientation$ Vertical=�6   .+�5      &   System.Windows.Controls.DataGrid  $��Stretch=�6      AutoGenerateColumns$ False��6e   $��Italic$�6�    �� SelectedItem 6=   �� *SelectedLogins+��6�   �� *Logins+	   Columns	 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn
   Header$

 Role��61   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6?   �� *role+  5      $
 Login��61    6@   �� *login+  5      $
 Password��61    6C   �� *
password+5       �� 5"      $ 
Horizontal=�6!   $��Center=�6:   .+�5#      �� $��Update��6!   ��62   �� *LoginsUpdateButton+�� 5$      $��Delete��6!   ��62   �� *LoginsDeleteButton+5%        5'      -   5          Name$ RoleDataGrid��5'      $��Stretch=�66   $ False��6~   $��Italic$�6�    6T   �� *SelectedNameRole+��6�   �� *Roles+	 5(        5)      $

 Role��61    6?   �� *role+5+      �� 5-      $ 
Horizontal=�6!   $��Center=�6:   .+�5.      �� $	��Add��6!   ��6/   �� *RoleAddButton+�� 5/      $��Update��6!   ��62   �� *RoleUpdateButton+�� 50      $��Delete��6!   ��62   �� *RoleDeleteButton+51      53      54      �� 56      $��	Hospitals��6   $��Stretch=�6)   .��57      �� $ Vertical=�6   .+�59        -   5        $ HospitalDataGrid��59      $ False��6g   $��Italic$�6�    69   �� *SelectedNameHospital+��6�   �� *	Hospitals+	 5:        5;      $
 Hospital��61    6C   �� *name+  5<      $
 Location��61    6C   �� *
location+5>      �� 5@      $ 
Horizontal=�6!   $��Center=�6:   .+�5A      �� $	��Add��6!   ��6/   �� *HospitalAddButton+�� 5B      $��Delete��6!   ��62   �� *HospitalDeleteButton+�� 5C      $��Update��6!   ��62   �� *HospitalUpdateButton+5D      5F      5G      �� 5I      $��Appointments��6   $��Stretch=�6,   .��5K      �� $ Vertical=�6   .+�5M        -   5        $ AppointmentsDataGrid��5M      $ False��6=   $��Italic$�6Y   $	��194��6�   ��6l   �� *Appointments+	 5N        5O      $
 Name doctor��61    6F   �� *doctorsname+  5P      $
 Specisalisation��61    6J   �� *specialisation+  5Q      $

 Date��61    6?   �� *date+  5R      $

 Time��61    6?   �� *time+  5S      $
 Cabinet��61    6B   �� *	cabinet+  5T      $
 Patient��61    6B   �� *patientname+5V        5X      -   5        $ CountAppointmentsDataGrid��5X      $ False��6B   $��Italic$�6^   $	��186��6�   ��6q   �� *CountAppointment+	 5Y        5Z      $
 Lastname��61    6C   �� *lname+  5[      $
 	Firstname��61    6D   �� *fname+  5\      $
 Surname��61    6B   �� *	surname+  5]      $
 Specialisation��61    6I   �� *spec+  5^      $
 Cabinet��61    6B   �� *cab+  5_      $
 Free appointments��61    6L   �� *cou+5a      5c      5d      �� 5f      $��Right=�6   ��5g      �� 5h      $��Right=�6   $��Log out��69   ��6K   �� *LogOutButton+��6l   ��  �� ElementName$ adminWindow��5j      5k      5l      5m      