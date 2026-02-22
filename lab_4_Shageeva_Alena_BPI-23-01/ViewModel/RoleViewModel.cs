using lab_4_Shageeva_Alena_BPI_23_01.Helper;
using lab_4_Shageeva_Alena_BPI_23_01.Model;
using lab_4_Shageeva_Alena_BPI_23_01.View;
//using lab_4_Shageeva_Alena_BPI_23_01.Helper;
//using lab_4_Shageeva_Alena_BPI_23_01.Model;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;

namespace lab_4_Shageeva_Alena_BPI_23_01.ViewModel
{
    public class RoleViewModel : INotifyPropertyChanged
    {
        //readonly string path = @"C:\Users\minig\source\repos\lab_4-1_MinigareevaAlena\lab_4-1_MinigareevaAlena\DataModels\RoleData.json";
        private readonly string path;

        private Role selectedRole;
        public Role SelectedRole
        {
            get => selectedRole;
            set
            {
                selectedRole = value;
                OnPropertyChanged("SelectedRole");
            }
        }

        public ObservableCollection<Role> ListRole { get; set; } = new ObservableCollection<Role>();
        public string Error { get; set; }
        string _jsonRoles = String.Empty;

        public RoleViewModel()
        {
            
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string projectRoot = Path.GetFullPath(Path.Combine(baseDir, @"..\..\"));
            path = Path.Combine(projectRoot, "DataModels", "RoleData.json");

            
            ListRole = new ObservableCollection<Role>();

            
            if (File.Exists(path))
            {
                
                ListRole = LoadRole();
            }
            else
            {
                
                string errorMessage = $"Файл с данными не найден по пути: {path}. Будут использоваться тестовые данные.";
                MessageBox.Show(errorMessage, "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);

             
                Error = errorMessage;

            }
        }

        #region command AddRole
        private RelayCommand addRole;
        public RelayCommand AddRole
        {
            get
            {
                return addRole ??
                    (addRole = new RelayCommand(obj =>
                    {
                        WindowNewRole wnRole = new WindowNewRole
                        {
                            Title = "Новая должность",
                        };
                        int maxIdRole = MaxId() + 1;
                        Role role = new Role { Id = maxIdRole };
                        wnRole.DataContext = role;
                        if (wnRole.ShowDialog() == true)
                        {
                            ListRole.Add(role);
                            SaveChanges(ListRole);
                        }
                        SelectedRole = role;
                    },
                    (obj) => true));
            }
        }
        #endregion

        #region Command EditRole
        private RelayCommand editRole;
        public RelayCommand EditRole
        {
            get
            {
                return editRole ??
                    (editRole = new RelayCommand(obj =>
                    {
                        WindowNewRole wnRole = new WindowNewRole
                        {
                            Title = "Редактирование должности",
                        };
                        Role role = SelectedRole;
                        Role tempRole = new Role();
                        tempRole = role.ShallowCopy();
                        wnRole.DataContext = tempRole;
                        if (wnRole.ShowDialog() == true)
                        {
                            role.NameRole = tempRole.NameRole;
                            SaveChanges(ListRole);
                        }
                    }, (obj) => SelectedRole != null && ListRole.Count > 0));
            }
        }
        #endregion

        #region DeleteRole
        private RelayCommand deleteRole;
        public RelayCommand DeleteRole
        {
            get
            {
                return deleteRole ??
                    (deleteRole = new RelayCommand(obj =>
                    {
                        Role role = SelectedRole;
                        MessageBoxResult result = MessageBox.Show("Удалить данные по должности: " +
                            role.NameRole, "Предупреждение", MessageBoxButton.OKCancel,
                            MessageBoxImage.Warning);
                        if (result == MessageBoxResult.OK)
                        {
                            ListRole.Remove(role);
                            SaveChanges(ListRole);
                        }
                    }, (obj) => SelectedRole != null && ListRole.Count > 0));
            }
        }
        #endregion

        #region Methods
        public ObservableCollection<Role> LoadRole()
        {
            _jsonRoles = File.ReadAllText(path);
            if (_jsonRoles != null)
            {
                ListRole = JsonConvert.DeserializeObject<ObservableCollection<Role>>(_jsonRoles);
                return ListRole;
            }
            else
            {
                return null;
            }
        }

        public int MaxId()
        {
            int max = 0;
            foreach (var r in this.ListRole)
            {
                if (max < r.Id)
                {
                    max = r.Id;
                }
                ;
            }
            return max;
        }

        private void SaveChanges(ObservableCollection<Role> listRole)
        {
            var jsonRole = JsonConvert.SerializeObject(listRole);
            try
            {
                using (StreamWriter writer = File.CreateText(path))
                {
                    writer.Write(jsonRole);
                }
            }
            catch (IOException e)
            {
                Error = "Ошибка записи json файла \n" + e.Message;
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}