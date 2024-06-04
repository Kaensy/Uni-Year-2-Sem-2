package iss.backend.Service;


import iss.Models.*;
import iss.backend.Repository.*;
import lombok.AllArgsConstructor;
import lombok.NoArgsConstructor;

import java.util.List;

@AllArgsConstructor
@org.springframework.stereotype.Service
public class Service {


    private RepoAccount repoAccount;
    private RepoAdmin repoAdmin;
    private RepoBook repoBook;
    private RepoBorrowRequest repoBorrowRequest;
    private RepoLibrarian repoLibrarian;
    private RepoSubscriber repoSubscriber;
    private RepoLibrary repoLibrary;

    public Account findAccountById(Long id) {
        return repoAccount.findById(id).orElse(null);
    }

    public Account saveAccount(Account account) {
        return repoAccount.save(account);
    }

    public void deleteAccount(Long id) {
        repoAccount.deleteById(id);
    }

    public List<Account> getAllAccounts() {
        return repoAccount.findAll();
    }

    public Admin saveAdmin(Admin admin) {
        return repoAdmin.save(admin);
    }

    public Admin findAdminById(Long id) {
        return repoAdmin.findById(id).orElse(null);
    }

    public void deleteAdmin(Long id) {
        repoAdmin.deleteById(id);
    }

    public List<Admin> getAllAdmins() {
        return repoAdmin.findAll();
    }

    public Subscriber saveSubscriber(Subscriber subscriber) {
        return repoSubscriber.save(subscriber);
    }

    public Subscriber findSubscriberById(Long id) {
        return repoSubscriber.findById(id).orElse(null);
    }

    public void deleteSubscriber(Long id) {
        repoSubscriber.deleteById(id);
    }

    public List<Subscriber> getAllSubscribers() {
        return repoSubscriber.findAll();
    }

    public Librarian saveLibrarian(Librarian librarian) {
        return repoLibrarian.save(librarian);
    }

    public Librarian findLibrarianById(Long id) {
        return repoLibrarian.findById(id).orElse(null);
    }

    public void deleteLibrarian(Long id) {
        repoLibrarian.deleteById(id);
    }

    public List<Librarian> getAllLibrarians() {
        return repoLibrarian.findAll();
    }

    public Book saveBook(Book book) {
        return repoBook.save(book);
    }

    public Book findBookById(Long id) {
        return repoBook.findById(id).orElse(null);
    }

    public void deleteBook(Long id) {
        repoBook.deleteById(id);
    }

    public List<Book> getAllBooks() {
        return repoBook.findAll();
    }

    public BorrowRequest saveBorrowRequest(BorrowRequest borrowRequest) {
        return repoBorrowRequest.save(borrowRequest);
    }

    public BorrowRequest findBorrowRequestById(Long id) {
        return repoBorrowRequest.findById(id).orElse(null);
    }

    public void deleteBorrowRequest(Long id) {
        repoBorrowRequest.deleteById(id);
    }

    public List<BorrowRequest> getAllBorrowRequests() {
        return repoBorrowRequest.findAll();
    }

    public Library saveLibrary(Library library) {
        return repoLibrary.save(library);
    }

    public Library findLibraryById(Long id) {
        return repoLibrary.findById(id).orElse(null);
    }

    public void deleteLibrary(Long id) {
        repoLibrary.deleteById(id);
    }

    public List<Library> getAllLibraries() {
        return repoLibrary.findAll();
    }

}
