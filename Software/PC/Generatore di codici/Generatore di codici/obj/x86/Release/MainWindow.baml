   M S B A M L   `   `   `  ����   Generatore di codici%    Generatore_di_codici.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$  
MainWindow��$	��430��$	��881��.��� $	��786��$	��378��.+��� -   $��txtHash��$��23��$��Right=�$��
0,140,12,0q�$	��Top=�$	��717�� �� 
IsReadOnly .  �� -   $��label1��$,��&Codice macchina o MacAddress MAIUSCOLO��$��28��$
��Left=�$��
14,112,0,0q�$	��Top=�(  "System.Windows.Controls.DatePicker  -   $��datePickStart��$��25��$
��Left=�$��
14,186,0,0q�$	��Top=�$	��176��   SelectedDateFormat$ Short=��� -   $��label2��$��Data minima di utilizzo��$��28��$
��Left=�$��
14,163,0,0q�$	��Top=��� -   $��txtCBC��$��23��$
��Left=�$��
94,217,0,0q�$	��Top=�$	��680�� . �� -   $��txtCheckSum��$��23��$
��Left=�$��
94,246,0,0q�$	��Top=�$	��680�� . �� -   $��txtCompleteCode��$��24��$
��Left=�$��
14,313,0,0q�$	��Top=�$	��760�� . �� -	   $��label3��$J��DCodice completo (Hash codice macchina + RijndaelCBC data + CheckSum)��$��28��$
��Left=�$��
14,291,0,0q�$	��Top=��� -
   $��txtMachineOrMacAddrCode��$��23��$
��Left=�$��268,112,0,0q�$	��Top=�$	��506���� -   $��btnCalculate��$��	Ricalcola��$��23��$
��Left=�$��699,343,0,0q�$	��Top=�$��75���� -   $��label4��$
��Hash��$��28��$
��Left=�$��
14,138,0,0q�$	��Top=��� -   $��label5��$��RijndaelCBC��$��28��$
��Left=�$��
14,215,0,0q�$	��Top=��� -   $��label6��$��CheckSum��$��28��$
��Left=�$��
14,246,0,0q�$	��Top=��� -   $��btnTest��$��Test Codice��$��23��$
��Left=�$��610,343,0,0q�$	��Top=�$��75���� -   $��textBox6��$��23��$��Right=� .  $��
0,54,423,0q�$	��Top=�$	��153��$��	127.0.0.1���� -   $��label7��$��MacAddress da IP��$��28��$
��Left=�$��	99,51,0,0q�$	��Top=��� -   $��btnLoadMacAddr��$��Carica��$��23��$
��Left=�$��	18,53,0,0q�$	��Top=�$��75���� -   $��btnLoadMachineCode��$��Carica codice macchina��$��23��$
��Left=�$��	18,82,0,0q�$	��Top=�$	��172��  -   $��datePickEnd��$��25��$
��Left=�$��196,186,0,0q�$ Short=�$	��Top=�$	��176���� -   $��label8��$��Data scadenza��$��28��$
��Left=�$��196,163,0,0q�$	��Top=��� -   $��button1��$��Carica da codice fornito��$��30��$
��Left=�$��	18,12,0,0q�$	��Top=�$	��756���� -   $��	comboBox1��$��25��$
��Left=�$��
485,54,0,0q�$	��Top=�$	��289�� �� SelectedIndex$ 0��.@��� $'��!prima versione - nessuna versione���� $��plugin XmlConnector v1.0���� $��plugin S7Connector v1.0���� $��plugin 3dSimulator v1.0���� $!��QEye SmartCamera Cognex 1.0���� -   $��	checkBox1��$-��'Forza MAIUSCOLO (usare solo per plugin)��$��16��$
��Left=�$��
530,98,0,0q�$	��Top=� d� 	IsChecked$ FalseY�$	��256��