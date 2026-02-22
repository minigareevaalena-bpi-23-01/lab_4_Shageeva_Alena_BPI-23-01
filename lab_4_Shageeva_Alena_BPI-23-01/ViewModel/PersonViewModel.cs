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
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace lab_4_Shageeva_Alena_BPI_23_01.ViewModel
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        //readonly string path = @"C:\Users\minig\source\repos\lab_4-1_MinigareevaAlena\lab_4-1_MinigareevaAlena\DataModels\PersonData.json";
        private readonly string path;

        private PersonDPO selectedPersonDpo;
        private RoleViewModel roleViewModel;

        public PersonDPO SelectedPersonDpo
        {
            get { return selectedPersonDpo; }
            set
            {
                selectedPersonDpo = value;
                OnPropertyChanged("SelectedPersonDpo");
            }
        }

        public ObservableCollection<Person> ListPerson { get; set; }
        public ObservableCollection<PersonDPO> ListPersonDpo { get; set; }

        public ObservableCollection<Role> ListRole
        {
            get { return roleViewModel?.ListRole; }
        }

        string _jsonPersons = String.Empty;
        public string Error { get; set; }
        public string Message { get; set; }

        public PersonViewModel(RoleViewModel roleViewModel)
        {
            if (roleViewModel == null)
                throw new ArgumentNullException(nameof(roleViewModel));

            this.roleViewModel = roleViewModel;

            // относ путь
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string projectRoot = Path.GetFullPath(Path.Combine(baseDir, @"..\..\"));
            path = Path.Combine(projectRoot, "DataModels", "PersonData1.json");

            ListPerson = new ObservableCollection<Person>();
            ListPersonDpo = new ObservableCollection<PersonDPO>();
            //ListPerson = LoadPerson();
            //ListPersonDpo = GetListPersonDpo();
            if (File.Exists(path))
            {
               
                ListPerson = LoadPerson();
                ListPersonDpo = GetListPersonDpo();
            }
            else
            {
                
                Message = $"Файл с данными не найден по пути: {path}. Будут загружены пустые списки.";

                
                MessageBox.Show(Message, "Ошибка загрузки", MessageBoxButton.OK, MessageBoxImage.Warning);

                
                Error = Message;

            }
        }

        #region AddPerson
        private RelayCommand addPerson;
        public RelayCommand AddPerson
        {
            get
            {
                return addPerson ??
                    (addPerson = new RelayCommand(obj =>
                    {
                        WindowNewEmployee wnPerson = new WindowNewEmployee(this.ListRole)
                        {
                            Title = "Новый сотрудник"
                        };

                        int maxIdPerson = MaxId() + 1;
                        PersonDPO per = new PersonDPO
                        {
                            Id = maxIdPerson,
                            Birthday = DateTime.Now.ToString("dd.MM.yyyy")
                        };

                        wnPerson.DataContext = per;

                        if (wnPerson.ShowDialog() == true)
                        {
                            Role r = (Role)wnPerson.CbRole.SelectedItem;
                            if (r != null)
                            {
                                per.RoleName = r.NameRole;
                                per.RoleId = r.Id;
                                ListPersonDpo.Add(per);

                                Person p = new Person();
                                p = p.CopyFromPersonDPO(per);
                                ListPerson.Add(p);

                                try
                                {
                                    SaveChanges(ListPerson);
                                }
                                catch (Exception e)
                                {
                                    Error = "Ошибка добавления данных в json файл\n" + e.Message;
                                }
                            }
                        }
                    },
                    (obj) => true));
            }
        }
        #endregion

        #region EditPerson
        private RelayCommand editPerson;
        public RelayCommand EditPerson
        {
            get
            {
                return editPerson ??
                    (editPerson = new RelayCommand(obj =>
                    {
                        WindowNewEmployee wnPerson = new WindowNewEmployee(this.ListRole)
                        {
                            Title = "Редактирование данных сотрудника",
                        };

                        PersonDPO personDpo = SelectedPersonDpo;
                        PersonDPO tempPerson = new PersonDPO();
                        tempPerson = personDpo.ShallowCopy();
                        wnPerson.DataContext = tempPerson;

                        if (wnPerson.ShowDialog() == true)
                        {
                            Role r = (Role)wnPerson.CbRole.SelectedItem;
                            if (r != null)
                            {
                                personDpo.RoleName = r.NameRole;
                                personDpo.RoleId = r.Id;
                                personDpo.FirstName = tempPerson.FirstName;
                                personDpo.LastName = tempPerson.LastName;
                                personDpo.Birthday = tempPerson.Birthday;

                                Person p = ListPerson.FirstOrDefault(x => x.Id == personDpo.Id);
                                if (p != null)
                                {
                                    p = p.CopyFromPersonDPO(personDpo);
                                }

                                try
                                {
                                    SaveChanges(ListPerson);
                                }
                                catch (Exception e)
                                {
                                    Error = "Ошибка редактирования данных в json файл\n" + e.Message;
                                }
                            }
                            else
                            {
                                Message = "Необходимо выбрать должность сотрудника.";
                            }
                        }
                    }, (obj) => SelectedPersonDpo != null && ListPersonDpo.Count > 0));
            }
        }
        #endregion

        #region DeletePerson
        private RelayCommand deletePerson;
        public RelayCommand DeletePerson
        {
            get
            {
                return deletePerson ??
                    (deletePerson = new RelayCommand(obj =>
                    {
                        PersonDPO person = SelectedPersonDpo;
                        MessageBoxResult result = MessageBox.Show("Удалить данные по сотруднику: \n" +
                            person.LastName + " " + person.FirstName,
                            "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                        if (result == MessageBoxResult.OK)
                        {
                            try
                            {
                                ListPersonDpo.Remove(person);

                                Person per = ListPerson.FirstOrDefault(p => p.Id == person.Id);
                                if (per != null)
                                {
                                    ListPerson.Remove(per);
                                    SaveChanges(ListPerson);
                                }
                            }
                            catch (Exception e)
                            {
                                Error = "Ошибка удаления данных\n" + e.Message;
                            }
                        }
                    }, (obj) => SelectedPersonDpo != null && ListPersonDpo.Count > 0));
            }
        }
        #endregion

        #region Method
        public ObservableCollection<Person> LoadPerson()
        {
            try
            {
                _jsonPersons = File.ReadAllText(path);
                if (_jsonPersons != null)
                {
                    ListPerson = JsonConvert.DeserializeObject<ObservableCollection<Person>>(_jsonPersons);
                    return ListPerson;
                }
                else { return null; }
            }
            catch (Exception e)
            {
                e.Message.ToString();
                    return null;
                
            }
            
        }

        public ObservableCollection<PersonDPO> GetListPersonDpo()
        {
            foreach (var person in ListPerson)
            {
                PersonDPO p = new PersonDPO();
                p = p.CopyFromPerson(person);
                ListPersonDpo.Add(p);
            }
            return ListPersonDpo;
        }

        public int MaxId()
        {
            int max = 0;
            foreach (var r in this.ListPerson)
            {
                if (max < r.Id)
                {
                    max = r.Id;
                }
                ;
            }
            return max;
        }

        private void SaveChanges(ObservableCollection<Person> listPersons)
        {
           string jsonPerson = JsonConvert.SerializeObject(listPersons);
            try
            {
                using (StreamWriter writer = File.CreateText(path))
                {
                    writer.Write(jsonPerson);
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