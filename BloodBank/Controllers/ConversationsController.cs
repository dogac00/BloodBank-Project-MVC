using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BloodBank.Data;
using BloodBank.Models;
using Microsoft.AspNetCore.Identity;

namespace BloodBank.Controllers
{
    public class ConversationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<BloodBankUser> _userManager;

        public ConversationsController(ApplicationDbContext context, UserManager<BloodBankUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Conversations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Conversations.ToListAsync());
        }

        // GET: Conversations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conversation = await _context.Conversations
                .FirstOrDefaultAsync(m => m.ConversationId == id);
            if (conversation == null)
            {
                return NotFound();
            }

            return View(conversation);
        }

        public IActionResult Show()
        {
            var conversations = _context
                .Conversations
                .Where(c => c.PostOwnerName == GetCurrentUser().UserName);

            return View(conversations);
        }

        public IActionResult FetchMessage(string applier)
        {
            var conversation = _context
                .Conversations
                .Where(c => c.PostOwnerName == User.Identity.Name)
                .Where(c => c.ApplierName == applier)
                .FirstOrDefault();

            return View("Contact", conversation);
        }
        
        public IActionResult Contact(string postOwnerName)
        {
            var currentUserName = User.Identity.Name;

            Conversation conversation = _context.Conversations
                .Include("Messages")
                .FirstOrDefault(c => c.ApplierName == currentUserName
                                    && c.PostOwnerName == postOwnerName);

            if (conversation == null)
            {
                Conversation newConversation = new Conversation
                {
                    PostOwnerName = postOwnerName,
                    ApplierName = currentUserName,
                    Messages = new List<Message>()
                };

                _context.Conversations.Add(newConversation);
                _context.SaveChanges();

                return View(newConversation);
            }

            return View(conversation);
        }

        public IActionResult AddMessage(string PostOwnerName, string ApplierName, string Content)
        {
            var conversation = _context
                .Conversations
                .Include("Messages")
                .Where(c => c.PostOwnerName == PostOwnerName)
                .Where(c => c.ApplierName == ApplierName)
                .FirstOrDefault();

            var message = new Message
            {
                Content = Content,
                Owner = GetCurrentUser()
            };

            conversation.Messages.Add(message);
            _context.Conversations.Update(conversation);
            _context.SaveChanges();

            return Contact(PostOwnerName);
        }

        private BloodBankUser GetCurrentUser()
        {
            if (User == null) return null;

            var userName = User.Identity.Name;
            var _currentUser = userName == null ? null : _userManager.FindByNameAsync(User.Identity.Name).Result;

            return _currentUser;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConversationId,FirstUserName,SecondUserName")] Conversation conversation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(conversation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(conversation);
        }

        // GET: Conversations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conversation = await _context.Conversations.FindAsync(id);
            if (conversation == null)
            {
                return NotFound();
            }
            return View(conversation);
        }

        // POST: Conversations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConversationId,FirstUserName,SecondUserName")] Conversation conversation)
        {
            if (id != conversation.ConversationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conversation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConversationExists(conversation.ConversationId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(conversation);
        }

        // GET: Conversations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conversation = await _context.Conversations
                .FirstOrDefaultAsync(m => m.ConversationId == id);
            if (conversation == null)
            {
                return NotFound();
            }

            return View(conversation);
        }

        // POST: Conversations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var conversation = await _context.Conversations.FindAsync(id);
            _context.Conversations.Remove(conversation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConversationExists(int id)
        {
            return _context.Conversations.Any(e => e.ConversationId == id);
        }
    }
}
