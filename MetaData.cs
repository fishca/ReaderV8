using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace MetaRead
{
    #region Перечисления

    //---------------------------------------------------------------------------
    // Версии контейнера 1С
    // значения версий должны обязательно располагаться по возрастанию, чтобы можно было сравнивать версии на >, < и =
    public enum ContainerVer
    {
        cv_2_0   = 1,
        cv_5_0   = 2,
        cv_6_0   = 3,
        cv_106_0 = 4,
        cv_200_0 = 5,
        cv_202_2 = 6,
        cv_216_0 = 7
    }

    //---------------------------------------------------------------------------
    // Версии 1С
    // значения версий должны обязательно располагаться по возрастанию, чтобы можно было сравнивать версии на >, < и =
    public enum Version1C
    {
        v1C_min    = 0,

        v1C_8_0    = 1,
        v1C_8_1    = 2,
        v1C_8_2    = 3,
        v1C_8_2_14 = 4,
        v1C_8_3_1  = 5,
        v1C_8_3_2  = 6,
        v1C_8_3_3  = 7,
        v1C_8_3_4  = 8,
        v1C_8_3_5  = 9,
        v1C_8_3_6  = 10
    }

    //---------------------------------------------------------------------------
    // Способы выгрузки
    public enum ExportType
    {
        et_default = 0,
        et_catalog = 1,
        et_file    = 2
    }

    //---------------------------------------------------------------------------
    // Виды значений по умолчанию
    public enum DefaultValueType
    {
        dvt_novalue = 0, // Нет значения по умолчанию
        dvt_bool    = 1, // Булево
        dvt_number  = 2, // Число
        dvt_string  = 3, // Строка
        dvt_date    = 4, // Дата
        dvt_undef   = 5, // Неопределено
        dvt_null    = 6, // Null
        dvt_type    = 7, // Тип
        dvt_enum    = 8 // Значение системного перечисления
    }

    //---------------------------------------------------------------------------
    // Типы узлов дерева сериализации
    public enum SerializationTreeNodeType
    {
        stt_min      = 0, // Минимум, для проверки

        stt_const    = 1, // Константа
        stt_var      = 2, // Переменная
        stt_list     = 3, // Список
        stt_prop     = 4, // Свойство
        stt_elcol    = 5, // ЭлементКоллекции
        stt_gentype  = 6, // ГенерируемыйТип
        stt_cond     = 7, // Условие
        stt_metaid   = 8, // МетаИД, идентификатор объекта метаданных
        stt_classcol = 9, // Коллекция классов
        stt_class    = 10, // Класс
        stt_idcol    = 11, // Коллекция ИД-элементов
        stt_idel     = 12, // ИД-элемент

        stt_max // Максимум, для проверки
    }

    //---------------------------------------------------------------------------
    // Типы значений дерева сериализации
    public enum SerializationTreeValueType
    {
        //stv_min     = 0, // Минимум, для проверки

        stv_string    = 1,  // Строка
        stv_number    = 2,  // Число
        stv_uid       = 3,  // УникальныйИдентификатор
        stv_value     = 4,  // Значение (MetaValue)
        stv_var       = 5,  // Переменная (SerializationTreeVar)
        stv_prop      = 6,  // Свойство (MetaProperty)
        stv_vercon    = 7,  // Версия контейнера
        stv_ver1C     = 8,  // Версия 1C
        stv_classpar  = 9,  // Параметр класса
        stv_globalvar = 10, // Глобальная переменная (SerializationTreeVar)
        stv_none      = 11  // Нет значения

        //stv_max // Максимум, для проверки
    }

    //---------------------------------------------------------------------------
    // Виды условий дерева сериализации
    public enum SerializationTreeCondition
    {
        stc_min = 0, // Минимум, для проверки

        stc_e   = 1, // Равно
        stc_ne  = 2, // НеРавно
        stc_l   = 3, // Меньше
        stc_g   = 4, // Больше
        stc_le  = 5, // МеньшеИлиРавно
        stc_ge  = 6, // БольшеИлиРавно
        stc_bs  = 7, // УстановленБит
        stc_bn  = 8, // НеУстановленБит

        stc_max // Максимум, для проверки
    }

    //---------------------------------------------------------------------------
    // Виды коллекций классов дерева сериализации
    public enum SerializationTreeClassType
    {
        stct_min       = 0, // Минимум, для проверки

        stct_inlist    = 1, // Классы в списке
        stct_notinlist = 2, // Классы не в списке

        stct_max // Максимум, для проверки
    }

    //---------------------------------------------------------------------------
    // Форматы внешних файлов типа (форматы двоичных данных)
    public enum ExternalFileFormat
    {
        eff_min       = 0,

        eff_servalue  = 1,  // СериализованноеЗначение
        eff_text      = 2,  // Текст (ИсходныйТекст)
        eff_tabdoc    = 3,  // ТабличныйДокумент
        eff_binary    = 4,  // ДвоичныеДанные
        eff_activedoc = 5,  // ActiveДокумент
        eff_htmldoc   = 6,  // HTMLДокумент
        eff_textdoc   = 7,  // ТекстовыйДокумент
        eff_geo       = 8,  // ГеографическаяСхема
        eff_kd        = 9,  // СхемаКомпоновкиДанных
        eff_mkd       = 10, // МакетОформленияКомпоновкиДанных
        eff_graf      = 11, // ГрафическаяСхема
        eff_xml       = 12, // XML
        eff_wsdl      = 13, // WSDL
        eff_picture   = 14, // Картинка
        eff_string    = 15, // Строка (строка длиной > maxStringLength)

        eff_max
    }

    //---------------------------------------------------------------------------
    // Типы сериализации двоичных данных
    public enum BinarySerializationType
    {
        bst_min            = 0,

        bst_empty          = 1, // БезПрефикса
        bst_base64         = 2, // Префикс base64
        bst_data           = 3, // Префикс data
        bst_base64_or_data = 4, // Префикс base64 или data (Преимущество base64)

        bst_max
    }

    //---------------------------------------------------------------------------
    // Виды значений 1С (для Value1C)
    public enum KindOfValue1C
    {
        kv_unknown,    // Неинициализированное значение
        kv_bool,       // Булево
        kv_string,     // Строка
        kv_number,     // Целое число
        kv_number_exp, // Число с плавающей запятой
        kv_date,       // Дата
        kv_null,       // Null
        kv_undef,      // Неопределено
        kv_type,       // Тип
        kv_uid,        // Уникальный идентификатор
        kv_enum,       // Системное перечисление
        kv_stdattr,    // Стандартный реквизит
        kv_stdtabsec,  // Стандартная табличная часть
        kv_obj,        // Объект
        kv_metaobj,    // Объект, являющийся объектом метаданных
        kv_refobj,     // Ссылка на объект, являющийся объектом метаданных
        kv_refpre,     // Ссылка на предопределенный элемент
        kv_right,      // Право
        kv_binary,     // Двоичные данные
        kv_extobj      // Внешний объект
    }

    #endregion

    //---------------------------------------------------------------------------
    // Базовый класс метаданных 1С
    public class MetaBase
    {
        public MetaBase() { }
        public MetaBase(String _name, String _ename)
        {
            Name = _name;
            Ename = _ename;
        }

        public String Name { get; set; }
        public String Ename { get; set; }

        public String GetName(bool english = false)
        {
            return english ? Ename : Name;
        }
    }


    //---------------------------------------------------------------------------
    // Предопределенное значение метаданных
    public class MetaValue : MetaBase
    {
        public MetaType owner;
        public int fvalue;
        Guid fvalueUID;

        public MetaValue()
        {
        }

        public MetaValue(MetaType _owner, String _name, String _ename, int _value) : base(_name, _ename)
        {
            fvalue = _value;
            owner = _owner;
        }
        public MetaValue(MetaType _owner, Tree tr)
        {
            owner = _owner;
        }

        public int Value
        {
            get
            {
                return fvalue;
            }
        }

        public Guid ValueUID
        {
            get
            {
                return fvalueUID;
            }
        }

        public MetaType GetOwner()
        { return owner; }

    }

    //---------------------------------------------------------------------------
    // Свойство метаданных
    public class MetaProperty : MetaBase
    {
        public List<MetaType> ftypes;
        public List<String> fstypes;
        public MetaType owner;
        public bool fpredefined;
        public ExportType fexporttype;
        public Class f_class;

        public DefaultValueType defaultvaluetype;
    }

    public class Class : MetaBase
    {
    }


    public class MetaGeneratedType : MetaBase { }


    //---------------------------------------------------------------------------
    // Переменная дерева сериализации
    public class SerializationTreeVar
    { }

    /// <summary>
    /// Тип метаданных
    /// </summary>
    public class MetaType : MetaBase
    {
        private void Init()
        {
        }

        public MetaTypeSet typeSet; // набор типов, которому принадлежит этот тип
        public Guid fuid;                  // УИД типа
        public bool fhasuid;               // Признак наличия УИД
        public String fmetaname;
        public String femetaname;
        public String fgentypeprefix;
        public String fegentypeprefix;

        public uint fserialization_ver; // Вариант сериализации
        public int fimageindex;         // индекс картинки
        public uint fprenameindex;      // Индекс колонки имя предопределенного
        public uint fpreidindex;        // ИндексКолонкиИДПредопределенного

        //public std::vector<MetaProperty*> fproperties; // Свойства

        //std::map<Key, Value> → SortedDictionary<TKey, TValue>
        //std::unordered_map<Key, Value> → Dictionary<TKey, TValue>

        public List<MetaProperty> fproperties; // Свойства

        public SortedDictionary<String, MetaProperty> fproperties_by_name; // Соответствие имен (русских и английских) свойствам

        public List<MetaValue> fvalues;  // Предопределенные значения типа

        public SortedDictionary<int, MetaValue> fvalues_by_value; // Соответствие числовых значений предопределенным значениям

        public SortedDictionary<String, MetaValue> fvalues_by_name; // Соответствие имен (русских и английских) предопределенным значениям

        public List<MetaType> fcollectiontypes; // Типы элементов коллекции
        public List<String> fscollectiontypes; // Имена типов элементов коллекции

        public List<MetaGeneratedType> fgeneratedtypes; // Генерируемые типы

        public bool fmeta; // Признак объекта метаданных

        public ExportType fexporttype;

        public Class fdefaultclass;
        
        // Дерево сериализации
        public List<SerializationTreeVar> fserializationvars;
        public SerializationTreeNode fserializationtree; // Если NULL - дерева сериализации нет
        public List<ExternalFile> fexternalfiles;

        public MetaType(MetaTypeSet _typeSet, String _name, String _ename, String _metaname, String _emetaname, String _uid)
        { }

        public MetaType(MetaTypeSet _typeSet, String _name, String _ename, String _metaname, String _emetaname, Guid _uid)
        { }

        public MetaGeneratedType gentypeRef; // генерируемый тип Ссылка
        public MetaType(MetaTypeSet _typeSet, Tree tr)
        { }

        public String Metaname
        {
            get { return fmetaname; }
        }
        public String Emetaname
        {
            get { return femetaname; }
        }
        public String GenTypePrefix
        {
            get { return fgentypeprefix; }
        }
        public String EgenTypePrefix
        {
            get { return fegentypeprefix; }
        }

        public bool HasUid
        {
            get { return fhasuid; }
        }
        public uint Serialization_Ver
        {
            get { return fserialization_ver; }
        }
        public int ImageIndex
        {
            get { return fimageindex; }
        }
        public uint PreNameIndex
        {
            get { return fprenameindex; }
        }
        public uint PreIdIndex
        {
            get { return fpreidindex; }
        }
        public List<MetaProperty> Properties
        {
            get { return fproperties; }
        }
        public List<MetaValue> Values
        {
            get { return fvalues; }
        }

        public List<MetaType> CollectionTypes
        {
            get { return fcollectiontypes; }
        }

        public List<MetaGeneratedType> GeneratedTypes
        {
            get { return fgeneratedtypes; }
        }

        public MetaTypeSet TypeSet
        {
            get { return typeSet; }
        }

        public List<SerializationTreeVar> SerializationVars
        {
            get { return fserializationvars; }
        }

        public SerializationTreeNode SerializationTree
        {
            get { return fserializationtree; }
        }

        public List<ExternalFile> ExternalFiles
        {
            get { return fexternalfiles; }
        }

        public bool Meta
        {
            get { return fmeta; }
        }

        public ExportType ExportType
        {
            get { return fexporttype; }
        }

        public Class DefaultClass
        {
            get { return fdefaultclass; }
        }

        public MetaProperty GetProperty(String n)
        { return null; }

        public MetaProperty GetProperty(int index)
        { return null; }

        public MetaProperty GetValue(String n)
        { return null; }

        public MetaProperty GetValue(int value)
        { return null; }

        public int NumberOfProperties()
        { return 0; }

        public void FillCollectionTypes() { } // Заполнить типы элементов коллекции по их именам (по fscollectiontypes заполнить fcollectiontypes)

        public String GetMetaName(bool english = false)
        {
            return english ? femetaname : fmetaname;
        }


    }

    //---------------------------------------------------------------------------
    // Набор типов метаданных (статических или генерируемых)
    public class MetaTypeSet
    {
        public SortedDictionary<String, MetaType> mapname; // соответствие имен (русских и английских) типам
        public SortedDictionary<Guid, MetaType> mapuid;    // соответствие идентификаторов типам
        public List<MetaType> alltype;                     // массив всех типов


        public static MetaTypeSet staticTypes; // Cтатические типы
        // Пустой тип
        public static MetaType mt_empty;
        // Примитивные типы
        public static MetaType mt_string;
        public static MetaType mt_number;
        public static MetaType mt_bool;
        public static MetaType mt_date;
        public static MetaType mt_undef;
        public static MetaType mt_null;
        public static MetaType mt_type;
        // УникальныйИдентификатор
        public static MetaType mt_uid;
        // ОписаниеТипаВнутр
        public static MetaType mt_typedescrinternal;
        // Двоичные данные
        public static MetaType mt_binarydata;
        // Произвольный тип
        public static MetaType mt_arbitrary;
        // Корневой тип
        public static MetaType mt_container;
        public static MetaType mt_config;
        // Псевдо-тип Стандартный атрибут
        public static MetaType mt_standart_attribute;
        // Псевдо-тип Стандартная табличная часть
        public static MetaType mt_standart_tabular_section;
        // Значения частей даты для квалификатора даты
        public static MetaValue mv_datefractionsdate;
        public static MetaValue mv_datefractionstime;
        // Тип ЧастиДаты
        public static MetaType mt_datefractions;
        // Свойство ЧастиДаты типа КвалификаторыДаты
        public static MetaProperty mp_datefractions;
        // ОбъектМетаданных: ТабличнаяЧасть
        public static MetaType mt_tabularsection;
        // ОбъектМетаданных: Реквизит
        public static MetaType mt_attribute;
        // ОбъектМетаданныхСсылка
        public static MetaType mt_metaobjref;
        // ОбъектМетаданныхСсылкаВнутр
        public static MetaType mt_metarefint; // специальный тип для свойств с галочкой Ссылка в деревьях сериализации
                                               // ТабличнаяЧасть
        public static MetaType mt_tabsection;
        // МетаСсылка
        public static MetaType mt_metaref;

        public MetaTypeSet() { }
        public MetaType GetTypeByName(String n) { return null; } // Получить тип по имени
        public MetaType GetTypeByUID(Guid u) { return null; } // Получить тип по УИД

        public void FillAll() { }
        public void Add(MetaType t) { }

        public static void StaticTypesLoad(Stream str) { }
        public int Number() { return 0; }

        public MetaType GetType(int indes) { return null; }


    }


    public class Tree
    { }

    //---------------------------------------------------------------------------
    // Узел дерева сериализации
    public struct SerializationTreeNode
    { }

    //---------------------------------------------------------------------------
    // Внешний файл типа
    public struct ExternalFile
    { }




    class MetaData
    {
    }
}
