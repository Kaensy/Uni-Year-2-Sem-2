from datetime import datetime, timedelta

import numpy as np
import pandas as pd
import matplotlib.pyplot as plt
import seaborn as sns


def number_of_employees():
    df_employees = pd.read_csv('employees.csv')
    print('Number of employees : ', df_employees.shape[0])


def info():
    df_employees = pd.read_csv('employees.csv')
    print(df_employees.shape[1], df_employees.columns.to_list())


def completed_rows():
    df_employees = pd.read_csv('employees.csv')
    df_no_empty_fields = df_employees.dropna(how='any')
    num_rows_no_empty_fields = df_no_empty_fields.shape[0]
    print("Number of rows without any empty fields: ", num_rows_no_empty_fields)


def min_max_mean_values():
    df_employees = pd.read_csv('employees.csv')
    numeric_cols = df_employees.select_dtypes(include=[np.number]).columns.to_list()

    min_values = df_employees[numeric_cols].min()
    max_values = df_employees[numeric_cols].max()
    mean_values = df_employees[numeric_cols].mean()

    df_employees['Start Date'] = pd.to_datetime(df_employees['Start Date'])

    df_employees['Last Login Time'] = df_employees['Last Login Time'].apply(
        lambda x: (datetime.strptime(x, '%I:%M %p') - datetime.strptime('12:00 AM', '%I:%M %p')).seconds)

    columns = ['Start Date', 'Last Login Time']

    min_values_date = df_employees[columns].min()
    max_values_date = df_employees[columns].max()
    mean_values_date = df_employees[columns].mean()

    min_values_date['Start Date'] = min_values_date['Start Date'].date()
    max_values_date['Start Date'] = max_values_date['Start Date'].date()
    mean_values_date['Start Date'] = mean_values_date['Start Date'].date()

    min_values_date['Last Login Time'] = str(timedelta(seconds=int(min_values_date['Last Login Time'])))
    max_values_date['Last Login Time'] = str(timedelta(seconds=int(max_values_date['Last Login Time'])))
    mean_values_date['Last Login Time'] = str(timedelta(seconds=int(mean_values_date['Last Login Time'])))

    min_values = pd.concat([min_values, min_values_date])
    max_values = pd.concat([max_values, max_values_date])
    mean_values = pd.concat([mean_values, mean_values_date])

    result = pd.concat([min_values, max_values, mean_values], axis=1)
    result.columns = ['Minimum', 'Maximum', 'Median']
    print(result)


def unique_values():
    df_employees = pd.read_csv('employees.csv')
    non_numerical_columns = df_employees.select_dtypes(exclude=[np.number])

    unique_values = non_numerical_columns.nunique()

    print(unique_values)


def valori_lipsa():
    df_employees = pd.read_csv('employees.csv')
    print(df_employees.isnull().sum().to_string() + '\n')
    print(df_employees.fillna("N/A"))



def distributia_salariilor():
    df_employees = pd.read_csv('employees.csv')

    salary_bins = df_employees['Salary'].value_counts(bins=10, sort=False)

    plt.figure(figsize=(10, 6))
    plt.hist(df_employees['Salary'], bins=10, edgecolor='black')
    plt.title('Salary Distribution')
    plt.xlabel('Salary')
    plt.ylabel('Number of Employees')
    plt.show()


def distributia_salariilor_echipe():
    df_employees = pd.read_csv('employees.csv')

    bins = [25000, 50000, 75000, 100000, 125000, np.inf]
    labels = ['25k-50k', '50k-75k', '75k-100k', '100k-125k', '125k+']

    df_employees['Salary Group'] = pd.cut(df_employees['Salary'], bins=bins, labels=labels)

    plt.figure(figsize=(10, 6))
    sns.countplot(data=df_employees, x='Team', hue='Salary Group')
    plt.title('Salary Distribution by Team')
    plt.xlabel('Team')
    plt.ylabel('Number of Employees')
    plt.xticks(rotation=90)
    plt.show()


def outliners():
    df_employees = pd.read_csv('employees.csv')

    df_employees = df_employees.sort_values('Salary')

    bottom_5 = df_employees.head(5)
    top_5 = df_employees.tail(5)

    median_index = df_employees.shape[0] // 2
    middle_salaries = df_employees.iloc[median_index - 2: median_index + 2]

    df_employees_sample = df_employees.sample(frac=0.1)

    plt.figure(figsize=(10, 6))
    plt.scatter(df_employees_sample.index, df_employees_sample['Salary'], color='blue', label='Employees')

    plt.scatter(bottom_5.index, bottom_5['Salary'], color='red', label='Bottom 5 Salaries')
    plt.scatter(top_5.index, top_5['Salary'], color='green', label='Top 5 Salaries')
    plt.scatter(middle_salaries.index, middle_salaries['Salary'], color='purple', label='Middle Salaries')

    plt.title('Salaries of Employees')
    plt.xlabel('Employee Index')
    plt.ylabel('Salary')
    plt.legend()
    plt.show()

if __name__ == '__main__':
    number_of_employees()
    info()
    completed_rows()
    min_max_mean_values()
    unique_values()
    valori_lipsa()
    distributia_salariilor()
    distributia_salariilor_echipe()
    outliners()
